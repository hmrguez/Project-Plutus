module UsersControllerTests

open System.Collections.Generic
open Xunit
open FluentAssertions
open Moq
open Project_Plutus.Controllers
open Project_Plutus.Interfaces
open Project_Plutus.Models
open Microsoft.AspNetCore.Mvc

[<Collection("ControllerTests")>]
type UsersControllerTests() =
    let mockRepo = Mock.Of<IUserRepository>()
    let controller = UsersController(mockRepo)

    [<Fact>]
    member this.``Get should return all users``() =
        // Arrange
        let expectedUsers = List<User>([User("Alice", 25); User("Bob", 30)])
        Mock.Get(mockRepo).Setup(fun repo -> repo.GetAll()).Returns(expectedUsers) |> ignore

        // Act
        let result = controller.Get()

        // Assert
        result.Value.Should().BeEquivalentTo(expectedUsers, "")

    [<Fact>]
    member this.``GetById with valid name should return the user``() =
        // Arrange
        let expectedUser = User("Alice", 25)
        Mock.Get(mockRepo).Setup(fun repo -> repo.GetByName("Alice")).Returns(Some expectedUser) |> ignore

        // Act
        let result = controller.GetById("Alice") :?> OkObjectResult

        // Assert
        result.Value.Should().BeEquivalentTo(expectedUser, "")

    
    [<Fact>]
    member this.``Put with existing user should return NoContent``() =
        // Arrange
        let userToUpdate = User("Alice", 25)
        Mock.Get(mockRepo).Setup(fun repo -> repo.Update(userToUpdate)).Returns(true) |> ignore

        // Act
        let result = controller.Put("Alice", userToUpdate) :?> NoContentResult

        // Assert
        result.Should().BeOfType<NoContentResult>("")

    [<Fact>]
    member this.``Put with non-existing user should return NotFound``() =
        // Arrange
        let userToUpdate = User("Charlie", 35)
        Mock.Get(mockRepo).Setup(fun repo -> repo.Update(userToUpdate)).Returns(false) |> ignore

        // Act
        let result = controller.Put("Charlie", userToUpdate) :?> NotFoundObjectResult

        // Assert
        result.Should().BeOfType<NotFoundObjectResult>("")

    [<Fact>]
    member this.``Delete with existing user should return NoContent``() =
        // Arrange
        Mock.Get(mockRepo).Setup(fun repo -> repo.Delete("Alice")).Returns(true) |> ignore

        // Act
        let result = controller.Delete("Alice") :?> NoContentResult

        // Assert
        result.Should().BeOfType<NoContentResult>("")

    [<Fact>]
    member this.``Delete with non-existing user should return NotFound``() =
        // Arrange
        Mock.Get(mockRepo).Setup(fun repo -> repo.Delete("Charlie")).Returns(false) |> ignore

        // Act
        let result = controller.Delete("Charlie") :?> NotFoundObjectResult

        // Assert
        result.Should().BeOfType<NotFoundObjectResult>("")