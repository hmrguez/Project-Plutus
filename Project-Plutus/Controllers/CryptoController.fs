namespace Project_Plutus.Controllers

open System.Net.Http
open Microsoft.AspNetCore.Mvc
open Newtonsoft.Json
open Newtonsoft.Json.Linq

[<ApiController>]
[<Route("[controller]")>]
type CryptoController() =
    inherit ControllerBase()

    let httpClient = new HttpClient()

    [<HttpGet("{name}")>]
    member this.Get(name: string) =
        let response = httpClient.GetAsync($"https://api.coingecko.com/api/v3/simple/price?ids={name}&vs_currencies=usd").Result
        let content = response.Content.ReadAsStringAsync().Result
        let json = JObject.Parse(content)
        match json with
        | :? JObject when json.Count = 0 -> BadRequestObjectResult("JSON response is empty.") :> IActionResult
        | _ -> OkObjectResult(content) :> IActionResult