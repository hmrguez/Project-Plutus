module LegendsControllerTests

open System.Collections.Generic
open Microsoft.AspNetCore.Mvc
open Xunit
open FluentAssertions
open Moq
open Project_Plutus.Controllers
open Project_Plutus.Interfaces
open Project_Plutus.Models

[<Collection("LegendsController Tests")>]
type LegendsControllerTests() =
    let legendRepositoryMock = Mock<ILegendRepository>()
    let legendsController = LegendsController(legendRepositoryMock.Object)

    [<Fact>]
    member this.``Get should return all legends``() =
        // Arrange
        let legends = List<Legend>()
        legends.Add(Legend(1, "asd","asd","asd", "asd","asd", "asd", 12, "", false))
        legends.Add(Legend(2, "asd","asd","asd", "asd","asd", "asd", 12, "", false))
        legendRepositoryMock.Setup(fun x -> x.GetAllLegends()).Returns(legends) |> ignore

         // Act
        let result = legendsController.Get()

        // Assert
        result.StatusCode.Should().Be(200, "Because it was an OK result") |> ignore
        result.Value.Should().BeEquivalentTo(legends, "Because of mocking")

    [<Fact>]
    member this.``GetById should return a legend for a valid id``() =
        // Arrange
        let legend = Legend(1, "asd","asd","asd", "asd","asd", "asd", 12, "", false)
        legendRepositoryMock.Setup(fun x -> x.GetLegendById(1)).Returns(Some(legend)) |> ignore

        // Act
        let result = legendsController.GetById(1) :?> OkObjectResult

        // Assert
        result.StatusCode.Should().Be(200, "") |> ignore
        result.Value.Should().BeEquivalentTo(legend, "")

    [<Fact>]
    member this.``GetById should return NotFoundObjectResult for an invalid id``() =
         // Arrange
         legendRepositoryMock.Setup(fun x -> x.GetLegendById(1)).Returns(None) |> ignore

         // Act
         let result = legendsController.GetById(1) :?> NotFoundObjectResult

         // Assert
         result.StatusCode.Should().Be(404, "") |> ignore
         result.Value.Should().Be("No result was found", "")

    [<Fact>]
    member this.``Post should insert a legend``() =
        // Arrange
        let legend = Legend(1, "asd","asd","asd", "asd","asd", "asd", 12, "", false)

        // Act
        let result = legendsController.Post(legend)
         
        // Assert
        result.StatusCode.Should().Be(200, "")

    [<Fact>]
    member this.``Put should update a legend for a valid id``() =
         // Arrange
         let legend = Legend(1, "asd","asd","asd", "asd","asd", "asd", 12, "", false)
         legendRepositoryMock.Setup(fun x -> x.UpdateLegend(legend)).Returns(true) |> ignore

         // Act
         let result = legendsController.Put(1, legend) :?> NoContentResult

         // Assert
         result.StatusCode.Should().Be(204, "")

    [<Fact>]
    member this.``Put should return NotFoundObjectResult for an invalid id``() =
         // Arrange
         let legend = Legend(1, "asd","asd","asd", "asd","asd", "asd", 12, "", false)
         legendRepositoryMock.Setup(fun x -> x.UpdateLegend(legend)).Returns(false) |> ignore

         // Act
         let result = legendsController.Put(1, legend) :?> NotFoundObjectResult

         // Assert
         result.StatusCode.Should().Be(404, "")

    [<Fact>]
    member this.``Delete should remove a legend for a valid id``() =
         // Arrange
         legendRepositoryMock.Setup(fun x -> x.RemoveLegendById(1)).Returns(true) |> ignore

         // Act
         let result = legendsController.Delete(1) :?> NoContentResult

         // Assert
         result.StatusCode.Should().Be(204, "")

    [<Fact>]
    member this.``Delete should return NotFoundObjectResult for an invalid id``() =
         // Arrange
         legendRepositoryMock.Setup(fun x -> x.RemoveLegendById(1)).Returns(false) |> ignore

         // Act
         let result = legendsController.Delete(1) :?> NotFoundObjectResult

         // Assert
         result.StatusCode.Should().Be(404, "")