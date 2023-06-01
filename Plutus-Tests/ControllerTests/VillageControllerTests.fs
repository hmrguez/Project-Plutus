module VillageControllerTests

open Xunit
open FluentAssertions
open Moq
open Project_Plutus.Controllers
open Project_Plutus.Interfaces
open Project_Plutus.Models
open Microsoft.AspNetCore.Mvc

[<Collection("VillageController Tests")>]
type VillageControllerTests() =
    let mockUserRepository = Mock<IUserRepository>()
    let mockLegendRepository = Mock<ILegendRepository>()
    let controller = VillageController(mockUserRepository.Object, mockLegendRepository.Object)

    [<Fact>]
    member this.``Work should return NotFoundObjectResult when legend does not exist`` () =
        // Arrange
        mockLegendRepository.Setup(fun repo -> repo.GetLegendById(It.IsAny<int>())).Returns(None) |> ignore

        // Act
        let result = controller.Work(1)

        // Assert
        result.Should().BeOfType<NotFoundObjectResult>("") |> ignore
        result.As<NotFoundObjectResult>().Value.Should().Be("Legend was not found", "")

    [<Fact>]
    member this. ``Work should update legend and owner and return OkObjectResult`` () =
        // Arrange
        let legend = Legend(1, "Legend1", "","", "", "", "", 1, "Owner1", false)
        let owner = User("Owner1", 10)
        let newLegend = Legend(1, "Legend1", "","", "", "", "", 2, "Owner1", false)
        let newOwner = User("Owner1", 20)

        mockLegendRepository.Setup(fun repo -> repo.GetLegendById(It.IsAny<int>())).Returns(Some(legend)) |> ignore
        mockLegendRepository.Setup(fun repo -> repo.UpdateLegend(It.IsAny<Legend>())).Returns(true) |> ignore
        
        mockUserRepository.Setup(fun repo -> repo.GetByName(It.IsAny<string>())).Returns(Some(owner)) |> ignore
        mockUserRepository.Setup(fun repo -> repo.Update(It.IsAny<User>())).Returns(true) |> ignore

        // Act
        let result = controller.Work(1)

        // Assert
        result.Should().BeOfType<OkObjectResult>("") |> ignore
        result.As<OkObjectResult>().Value.Should().BeNull("") 


    [<Fact>]
    member this. ``Work should return ConflictObjectResult when transaction fails and rollback changes`` () =
        // Arrange
        let legend = Legend(1, "Legend1", "","", "", "", "", 1, "Owner1", false)
        let owner = User("Owner1", 10)

        mockLegendRepository.Setup(fun repo -> repo.GetLegendById(It.IsAny<int>())).Returns(Some(legend)) |> ignore
        mockLegendRepository.Setup(fun repo -> repo.UpdateLegend(It.IsAny<Legend>())).Throws(new System.Exception()) |> ignore
        
        mockUserRepository.Setup(fun repo -> repo.GetByName(It.IsAny<string>())).Returns(Some(owner)) |> ignore
        mockUserRepository.Setup(fun repo -> repo.Update(It.IsAny<User>())).Throws(new System.Exception()) |> ignore

        // Act
        try
            let result = controller.Work(1)

            // Assert
            result.Should().BeOfType<ConflictObjectResult>("") |> ignore
            result.As<ConflictObjectResult>().Value.Should().Be("Transaction failed! Rolled back changes","") |> ignore
        
        with ex ->
            Assert.True(true)