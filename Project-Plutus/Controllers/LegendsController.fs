namespace Project_Plutus.Controllers

open Microsoft.AspNetCore.Mvc
open Project_Plutus.Interfaces
open Project_Plutus.Models

[<ApiController>]
[<Route("[controller]")>]
type LegendsController(legendRepository : ILegendRepository) =
    inherit ControllerBase()

    [<HttpGet>]
    member this.Get() =
        legendRepository.GetAllLegends() |> OkObjectResult

     
    [<HttpGet("{id}")>]
    member this.GetById(id: int) =
        let result = legendRepository.GetLegendById(id)
        match result with
        | Some legend -> OkObjectResult legend :> IActionResult
        | None -> NotFoundObjectResult "No result was found" :> IActionResult

    [<HttpPost>]
    member this.Post(legend: Legend) =
        legendRepository.InsertLegend(legend)
        OkObjectResult()

    [<HttpPut("{id}")>]
    member this.Put(id:int, [<FromBody>]legend: Legend) =
        let updatedRows = legendRepository.UpdateLegend(legend)
        if not updatedRows then NotFoundObjectResult() :> IActionResult
        else NoContentResult() :> IActionResult

    [<HttpDelete("{id}")>]
    member this.Delete(id: int) =
        let deletedRows = legendRepository.RemoveLegendById(id)
        if not deletedRows then NotFoundObjectResult() :> IActionResult
        else NoContentResult() :> IActionResult