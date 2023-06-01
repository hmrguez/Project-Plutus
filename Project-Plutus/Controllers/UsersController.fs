namespace Project_Plutus.Controllers

open Microsoft.AspNetCore.Mvc
open Project_Plutus.Interfaces
open Project_Plutus.Models

[<ApiController>]
[<Route("[controller]")>]
type UsersController(userRepository : IUserRepository) =
    inherit ControllerBase()

    [<HttpGet>]
    member this.Get() =
        userRepository.GetAll() |> OkObjectResult
     
    [<HttpGet("{name}")>]
    member this.GetById(name: string) =
        let result = userRepository.GetByName(name)
        match result with
        | Some user -> OkObjectResult user :> IActionResult
        | None -> NotFoundObjectResult "No result was found" :> IActionResult

    [<HttpPost>]
    member this.Post(user: User) =
        let result = userRepository.Insert(user)
        if result then OkObjectResult() :> IActionResult
        else BadRequestObjectResult("The user you tried to insert already exists") :> IActionResult

    [<HttpPut("{name}")>]
    member this.Put(name:string, [<FromBody>]user: User) =
        let updatedRows = userRepository.Update(user)
        if not updatedRows then NotFoundObjectResult() :> IActionResult
        else NoContentResult() :> IActionResult

    [<HttpDelete("{name}")>]
    member this.Delete(name: string) =
        let deletedRows = userRepository.Delete(name)
        if not deletedRows then NotFoundObjectResult() :> IActionResult
        else NoContentResult() :> IActionResult