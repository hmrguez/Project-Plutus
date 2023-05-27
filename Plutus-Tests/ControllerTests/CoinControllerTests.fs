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
        let result = controller.GetPrice(name)
        
        // Assert
        result.Should().NotBeNull("Because it was correct").And.BeOfType<OkObjectResult>("Because it was correct")
    
        
    [<Fact>]

    member this. ``Get Crypto Price for Invalid Name returns NotFound``() =
        // Arrange
        let name = "invalid-name"

        // Act
        let result = controller.GetPrice(name)

        // Assert
        result.Should().NotBeNull("Because it was correct").And.BeOfType<NotFoundObjectResult>("Because it was not a coin")
        
    [<Fact>]
    member this.``Get Crypto History for Bitcoin returns Ok with Price``() =
        // Arrange
        let name = "bitcoin"

        // Act
        let result = controller.GetHistory(name)
        
        // Assert
        result.Should().NotBeNull("Because it was correct").And.BeOfType<OkObjectResult>("Because it was correct")
    
        
    [<Fact>]

    member this. ``Get Crypto History for Invalid Name returns NotFound``() =
        // Arrange
        let name = "invalid-name"

        // Act
        let result = controller.GetHistory(name)

        // Assert
        result.Should().NotBeNull("Because it was correct").And.BeOfType<NotFoundObjectResult>("Because it was not a coin")

    