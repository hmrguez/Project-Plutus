module CoinControllerTests

open System.Net.Http
open Microsoft.AspNetCore.Mvc
open Project_Plutus.Controllers
open Xunit
open FluentAssertions

[<Collection("CoinController Tests")>]
type CoinControllerTests() =
    let httpClient = new HttpClient()
    let controller = new CoinController()

    [<Fact>]
    member this.``Get Crypto Price for Bitcoin returns Ok with Price``() =
        // Arrange
        let name = "bitcoin"

        // Act
        let result = controller.Get(name)
        
        // Assert
        result.Should().NotBeNull("Because it was correct").And.BeOfType<OkObjectResult>("Because it was correct")
    
        
    [<Fact>]

    member this. ``Get Crypto Price for Invalid Name returns BadRequest``() =
        // Arrange
        let name = "invalid-name"

        // Act
        let result = controller.Get(name)

        // Assert
        result.Should().NotBeNull("Because it was correct").And.BeOfType<BadRequestObjectResult>("Because it was not a coin")

    