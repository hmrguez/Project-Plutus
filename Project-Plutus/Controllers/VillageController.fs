namespace Project_Plutus.Controllers

open Microsoft.AspNetCore.Mvc
open Project_Plutus.Interfaces
open Project_Plutus.Models

[<ApiController>]
[<Route("[controller]")>]
type VillageController(userRepository: IUserRepository, legendRepository: ILegendRepository) =
    inherit ControllerBase()
    
    [<HttpPost("work/{id}")>]
    member this.Work(id: int) =
        let legend = legendRepository.GetLegendById(id)
        match legend with
        | None -> NotFoundObjectResult("Legend was not found") :> IActionResult
        | Some legend ->
            // Update the new legend and owner definitions, giving PCoins to the owner and adding xp to the legend
            let owner = userRepository.GetByName(legend.Owner).Value
            let newLegend = Legend (legend.Id, legend.Name, legend.Armor, legend.Weapon, legend.Race, legend.Specialization, legend.Pet, legend.ExpLevel + 1, legend.Owner, legend.ForSale)
            let newOwner = User(owner.Name, owner.PCoin + 10)
            try
                // We try updating the db
                legendRepository.UpdateLegend(newLegend) |> ignore
                userRepository.Update(newOwner) |> ignore
                OkObjectResult() :> IActionResult
            with ex ->
                // If something happened we rollback changes
                legendRepository.UpdateLegend(legend) |> ignore
                userRepository.Update(owner) |> ignore
                ConflictObjectResult("Transaction failed! Rolled back changes") :> IActionResult