namespace Project_Plutus.Controllers

open Microsoft.AspNetCore.Mvc
open Project_Plutus.Interfaces
open Project_Plutus.Models
open Project_Plutus.Models.Dtos

type BuyNftRequest = {
    legend: Legend
    user: User
}

[<ApiController>]
[<Route("[controller]")>]
type NftMarketController(userRepository : IUserRepository, legendRepository: ILegendRepository) =
    inherit ControllerBase()
    
    [<HttpPost("buy")>]
    member this.UserBuyNft(dto: UserBuyLegendDto) =
        
        // Get from the repositories
        let legendRequest = legendRepository.GetLegendById(dto.NftId)
        let userRequest = userRepository.GetByName(dto.UserName)
        
        // Pattern matching validation for the requests
        match legendRequest, userRequest with
        | None, _ -> NotFoundObjectResult("Legend doesn't exist") :> IActionResult
        | _, None -> NotFoundObjectResult("User doesn't exist") :> IActionResult
        | Some legend, Some user->
            if not legend.ForSale then BadRequestObjectResult("Legend isn't for sale") :> IActionResult
            elif user.PCoin < dto.Amount then BadRequestObjectResult("User doesn't have enough PCoins") :> IActionResult
            else this.BuyNftTransaction({ legend = legend; user = user }, dto.Amount)
               
    [<HttpPost("sell/{id}")>]
    member this.UserSellNft(id: int) =
        let legendRequest = legendRepository.GetLegendById(id)
        match legendRequest with
        | None -> NotFoundObjectResult("Legend doesn't exist") :> IActionResult
        | Some legend ->
            let legendPrime = Legend(legend.Id, legend.Name, legend.Armor, legend.Weapon, legend.Race, legend.Specialization, legend.Pet, legend.ExpLevel, legend.Owner, true)
            legendRepository.UpdateLegend(legendPrime) |> ignore
            OkObjectResult() :> IActionResult
            
    member this.BuyNftTransaction(request: BuyNftRequest, amount: int) : IActionResult =
        let legend = request.legend
        let user = request.user
        let owner = userRepository.GetByName(legend.Owner).Value
        
        try
            // Create a new user for the owner updating its PCoin with the incoming amount
            let owner = userRepository.GetByName(legend.Owner).Value
            let ownerPrime = User(owner.Name, owner.PCoin + amount)
            userRepository.Update(ownerPrime) |> ignore
            
            // Create a new user for the user updating its PCoin with the outgoing amount
            let userPrime = User(user.Name, user.PCoin - amount)
            userRepository.Update(userPrime) |> ignore
            
            // Changing the legend's owner
            let legendPrime = Legend(legend.Id, legend.Name, legend.Armor, legend.Weapon, legend.Race, legend.Specialization,
                                     legend.Pet, legend.ExpLevel, userPrime.Name, false)
            legendRepository.UpdateLegend(legendPrime) |> ignore
            
            OkObjectResult() :> IActionResult
 
        with ex ->
            
            // If an exception happened during the whole transaction we need to remain ACID,
            // so we rollback changes
            
            userRepository.Update(user) |> ignore
            userRepository.Update(owner) |> ignore
            legendRepository.UpdateLegend(legend) |> ignore
            
            ConflictObjectResult("The transaction wasn't successful and we rolled back changes") :> IActionResult