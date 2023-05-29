namespace Project_Plutus.Controllers

// open Project_Plutus.Controllers



open System
open System.IO
open System.Net
open System.Net.Http
open Microsoft.AspNetCore.Http.HttpResults
open Microsoft.AspNetCore.Mvc
open Newtonsoft.Json.Linq

type Coin =
    { id: string
      symbol: string
      name: string
      current_price: float
      price_change_percentage_24h_in_currency: float }

[<ApiController>]
[<Route("[controller]")>]
type CoinController() =
    inherit ControllerBase()

    let httpClient = new HttpClient()

    [<HttpGet("price/{name}")>]
    member this.GetPrice(name: string) =
        let response = httpClient.GetAsync($"https://api.coingecko.com/api/v3/simple/price?ids={name}&vs_currencies=usd").Result
        let content = response.Content.ReadAsStringAsync().Result
        let jsonResponse = JObject.Parse(content)
        if jsonResponse.HasValues then
            OkObjectResult(content) :> IActionResult
        else
            NotFoundObjectResult() :> IActionResult
    
    
    [<HttpGet("history/{name}")>]
    member this.GetHistory (name: string) =
        try
            let url = $"https://api.coingecko.com/api/v3/coins/{name}/market_chart?vs_currency=usd&days=7&interval=hourly"
            let responseTask = httpClient.GetStringAsync(url)
            let response = responseTask.Result
            let jsonResponse = JObject.Parse(response)
            if jsonResponse.HasValues then
                OkObjectResult(response) :> IActionResult
            else
                NotFoundObjectResult() :> IActionResult
        with
            | :? AggregateException as ex -> NotFoundObjectResult(ex.Message) :> IActionResult
     
     
    [<HttpGet("top")>]
    member this.GetTop() =
        let url = "https://api.coingecko.com/api/v3/coins/markets?vs_currency=usd&order=market_cap_desc&per_page=100&page=1&sparkline=false&price_change_percentage=24h"
        // let response = this.getContents(url)
        let responseTask = httpClient.GetStringAsync(url)
        let response = responseTask.Result
        
        let coins = JObject.Parse(response).ToObject<Coin[]>()
        let topCoins = coins
                       |> Array.sortByDescending (fun coin -> coin.price_change_percentage_24h_in_currency) 
                       |> Array.take 10
        
        OkObjectResult(topCoins) :> IActionResult
        
        
    