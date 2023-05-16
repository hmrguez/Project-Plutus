namespace Project_Plutus.Controllers

open System.Net.Http
open System.Threading.Tasks
open Microsoft.AspNetCore.Mvc
open Newtonsoft.Json

[<ApiController>]
[<Route("[controller]")>]
type CryptoController() =
    inherit ControllerBase()

    let httpClient = new HttpClient()

    [<HttpGet("{name}")>]
    member this.Get(name: string) =
        let response = httpClient.GetAsync($"https://api.coingecko.com/api/v3/simple/price?ids={name}&vs_currencies=usd").Result
        let content = response.Content.ReadAsStringAsync().Result
        try
            let jsonContent = JsonConvert.SerializeObject(content)
            OkObjectResult(jsonContent) :> IActionResult
        with
            | ex -> BadRequestObjectResult $"Error serializing content: %s{ex.Message}" :> IActionResult