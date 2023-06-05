namespace Project_Plutus.Controllers

open System.IO
open System.Net.Http
open System.Text
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Configuration
open Newtonsoft.Json.Linq
open Project_Plutus.Data

[<ApiController>]
[<Route("[controller]")>]
type SchoolController(config: IConfiguration) =
    inherit ControllerBase()
    
    let httpClient = new HttpClient()
    let apiKey = config.GetSection("NewsApiKey").Value
    
    [<HttpGet("news")>]
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
    
    [<HttpGet("learn")>]
    member this.GetLearningIndex() =
        let filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "School", "index")
        if not <| File.Exists(filePath) then
            NotFoundObjectResult() :> IActionResult
        else
            let fileContents = File.ReadAllText(filePath, Encoding.UTF8)
            OkObjectResult(fileContents) :> IActionResult
    
    [<HttpGet("learn/{topic}")>]
    member this.GetLearningInformation(topic: string) =
        let filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "School", topic)
        if not <| File.Exists(filePath) then
            NotFoundObjectResult() :> IActionResult
        else
            let fileContents = File.ReadAllText(filePath, Encoding.UTF8)
            OkObjectResult(fileContents) :> IActionResult
    