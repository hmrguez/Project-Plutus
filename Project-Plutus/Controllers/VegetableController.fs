namespace Project_Plutus.Controllers

open Microsoft.AspNetCore.Mvc

[<ApiController>]
[<Route("[controller]")>]
type VegetableController () =
    inherit ControllerBase()
    
    let vegetables = [| "carrot"; "broccoli"; "spinach"; "cabbage"; "cauliflower"; "tomato"; "potato" |]
    
    member this.GetRandomVegetable() =
        let random = System.Random()
        let index = random.Next(0, vegetables.Length)
        vegetables.[index]

    [<HttpGet("{index:int}")>]
    member this.Get(index: int) =
        if index >= 0 && index < vegetables.Length then
            let vegetable = vegetables.[index]
            OkObjectResult(vegetable) :> IActionResult
        else
            BadRequestObjectResult("Invalid Index") :> IActionResult

    [<HttpGet("myfavoritevegetable")>]
    member this.MyFavoriteVegetable() =
        OkObjectResult("broccoli") :> IActionResult