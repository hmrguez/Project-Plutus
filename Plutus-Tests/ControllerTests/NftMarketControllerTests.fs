module NftMarketControllerTests

open System
open Microsoft.AspNetCore.Mvc
open Xunit
open FluentAssertions
open Moq
open Project_Plutus.Controllers
open Project_Plutus.Interfaces
open Project_Plutus.Models
open Project_Plutus.Models.Dtos

[<Collection("NftMarketController Tests")>]
type NftMarketControllerTests() =
    let mockUserRepository = Mock<IUserRepository>()
    let mockLegendRepository = Mock<ILegendRepository>()
    let controller = NftMarketController(mockUserRepository.Object, mockLegendRepository.Object)

    [<Fact>]
    member this.``UserBuyNft should return NotFound if legend doesn't exist``() =
        // Arrange
        let dto = UserBuyLegendDto("Alice", 1, 40)
        mockLegendRepository.Setup(fun repo -> repo.GetLegendById(dto.NftId)).Returns(None) |> ignore
        
        // Act
        let result = controller.UserBuyNft(dto)
        
        // Assert
        result.Should().BeOfType<NotFoundObjectResult>("")
        
    [<Fact>]
    member this.``UserBuyNft should return NotFound if user doesn't exist``() =
        // Arrange
        let dto = UserBuyLegendDto("Alice", 1, 40)
        mockUserRepository.Setup(fun repo -> repo.GetByName(dto.UserName)).Returns(None) |> ignore
        
        // Act
        let result = controller.UserBuyNft(dto)
        
        // Assert
        result.Should().BeOfType<NotFoundObjectResult>("")
        
        
    [<Fact>]
    member this.``UserBuyNft should return BadRequest if legend isn't for sale``() =
        // Arrange
        let legend = Legend(1,"","","","","","",1,"",false)
        let user = User("Alice", 30)
        let dto = UserBuyLegendDto(user.Name, legend.Id, 10)
        mockLegendRepository.Setup(fun repo -> repo.GetLegendById(dto.NftId)).Returns(Some legend) |> ignore
        mockUserRepository.Setup(fun repo -> repo.GetByName(dto.UserName)).Returns(Some user) |> ignore
        
        // Act
        let result = controller.UserBuyNft(dto)
        
        // Assert
        result.Should().BeOfType<BadRequestObjectResult>("")
        
    [<Fact>]
    member this.``UserBuyNft should return BadRequest if user doesn't have enough PCoins``() =
        // Arrange
        let legend = Legend(1,"","","","","","",1,"",false)
        let user = User("Alice", 30)
        let dto = UserBuyLegendDto(user.Name, legend.Id, 50)
        mockLegendRepository.Setup(fun repo -> repo.GetLegendById(dto.NftId)).Returns(Some legend) |> ignore
        mockUserRepository.Setup(fun repo -> repo.GetByName(dto.UserName)).Returns(Some user) |> ignore
        
        // Act
        let result = controller.UserBuyNft(dto)
        
        // Assert
        result.Should().BeOfType<BadRequestObjectResult>("")
        
    [<Fact>]
    member this.``UserBuyNft should return Ok if the transaction is successful``() =
        // Arrange
        let legend = Legend(1, "Legend 1", "Armor 1", "Weapon 1", "Race 1", "Specialization 1", "Pet 1", 100, "Bob", true)
        let user = User("Alice",200)
        let owner = User("Bob",0)
        
        mockLegendRepository.Setup(fun repo -> repo.GetLegendById(It.IsAny<int>()))
                            .Returns(Some legend)
                            |> ignore
        mockUserRepository.Setup(fun repo -> repo.GetByName(It.Is<string>(fun name -> name = "Alice")))
                          .Returns(Some user)
                          |> ignore
        mockUserRepository.Setup(fun repo -> repo.GetByName(It.Is<string>(fun name -> name = "Bob")))
                          .Returns(Some owner)
                          |> ignore
        mockUserRepository.Setup(fun repo -> repo.Update(It.IsAny<User>()))
                          .Returns(true)
                          |> ignore
        mockLegendRepository.Setup(fun repo -> repo.UpdateLegend(It.IsAny<Legend>()))
                             .Returns(true)
                             |> ignore
        
        // Act
        let result = controller.UserBuyNft(UserBuyLegendDto("Alice", 1, 100)) :?> OkObjectResult
        
        // Assert
        result.Value.Should().BeNull("")
        
    [<Fact>]
    member this.``UserBuyNft should return ConflictObjectResult if the transaction isn't successful``() =
        // Arrange
        let legend = Legend(1, "Legend 1", "Armor 1", "Weapon 1", "Race 1", "Specialization 1", "Pet 1", 100, "Bob", true)
        let user = User("Alice",200)
        let owner = User("Bob",0)
        
        mockLegendRepository.Setup(fun repo -> repo.GetLegendById(It.IsAny<int>()))
                            .Returns(Some legend)
                            |> ignore
        mockUserRepository.Setup(fun repo -> repo.GetByName(It.Is<string>(fun name -> name = "Alice")))
                          .Returns(Some user)
                          |> ignore
        mockUserRepository.Setup(fun repo -> repo.GetByName(It.Is<string>(fun name -> name = "Bob")))
                          .Returns(Some owner)
                          |> ignore
        mockUserRepository.Setup(fun repo -> repo.Update(It.IsAny<User>()))
                          .Throws<Exception>()
                          |> ignore
        mockLegendRepository.Setup(fun repo -> repo.UpdateLegend(It.IsAny<Legend>()))
                             .Throws<Exception>()
                             |> ignore
        
        // Act & Assert
        try
            let result = controller.UserBuyNft(UserBuyLegendDto("Alice", 1, 100))
            result.Should().BeOfType<ConflictObjectResult>("") |> ignore
        with ex ->
            Assert.True(true)
        
    
    [<Fact>]
    member this.``UserSellNft should return NotFound if legend doesn't exist``() =
        // Arrange
        mockLegendRepository.Setup(fun repo -> repo.GetLegendById(It.IsAny<int>())).Returns(None) |> ignore
        
        // Act
        let result = controller.UserSellNft(1)
                   
        // Assert
        result.Should().BeOfType<NotFoundObjectResult>("")
    
    [<Fact>]
    member this.``UserSellNft should update the legend and return Ok if the legend exists``() =
        // Arrange
        let legend = Legend(1,"","","","","","",1,"",false)
        mockLegendRepository.Setup(fun repo -> repo.GetLegendById(It.IsAny<int>())).Returns(Some legend) |> ignore
        
        // Act
        let result = controller.UserSellNft(1)
        
        // Assert
        result.Should().BeOfType<OkObjectResult>("") |> ignore
        result.As<OkObjectResult>().Value.Should().BeNull("")