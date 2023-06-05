namespace Project_Plutus.Controllers

open System.Net.Http
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Configuration
open Newtonsoft.Json.Linq
open Project_Plutus.Data

[<ApiController>]
[<Route("[controller]")>]
type NewsController(config: IConfiguration) =
    inherit ControllerBase()
    
    let httpClient = new HttpClient()
    let apiKey = config.GetSection("NewsApiKey").Value
    
    [<HttpGet>]
    member this.GetPrice() =
        let response = httpClient.GetAsync($"http://api.mediastack.com/v1/news?access_key={apiKey}&categories=technology&keywords=blockchain").Result
        let content = response.Content.ReadAsStringAsync().Result
        let jsonResponse = JObject.Parse(content)
        
        let items = jsonResponse.SelectToken("data").ToObject<JArray>()
        let filteredItems = items |> Seq.map (fun item -> News(
                item.SelectToken("author").ToString(),
                item.SelectToken("title").ToString(),
                item.SelectToken("description").ToString()
            )
        )
        let limitedItems = filteredItems |> Seq.take(10)
        OkObjectResult(limitedItems) :> IActionResult
