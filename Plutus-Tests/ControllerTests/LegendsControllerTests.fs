// open System
// open System.Collections.Generic
// open Microsoft.AspNetCore.Mvc
// open Xunit
// open FluentAssertions
// open Project_Plutus.Controllers
// open Dapper

// let connString = "Server=localhost\\SQLEXPRESS;Database=PlutusDB;Encrypt=False;Trusted_Connection=True"
//
// [<Fact>]
// let ``Get method should return all Legends`` () =
//     // Arrange
//     let controller = LegendsController()
//     controller.SetConnectionString(connString)
//
//     // Act
//     let result = controller.Get()
//
//     // Assert
//     result.Should().NotBeNull("Because it was correct")
//     result.Should().NotBeEmpty("Because there are elements")
//
// [<Fact>]
// let ``GetById method should return a specific Legend`` () =
//     // Arrange
//     let controller = LegendsController()
//     controller.SetConnectionString(connString)
//     
//     System.Threading.Thread.Sleep(1000)
//        
//     let conn = controller.GetConnection()
//     let query = "SELECT * FROM Legends WHERE name = @name"
//     let legend = conn.QuerySingleOrDefault<Legend>(query, {| name = "Updated Legend" |})
//        
//     // Act
//     let result = controller.GetById(legend.Id)
//     
//     // Assert
//     result.Should().BeOfType<OkObjectResult>("Because it was an Ok result")
//
// [<Fact>]
// let ``GetById method should return NotFound for non-existent Legend`` () =
//     // Arrange
//     let controller = LegendsController()
//     controller.SetConnectionString(connString)
//
//     // Act
//     let result = controller.GetById(99)
//
//     // Assert
//     result.Should().BeOfType<NotFoundObjectResult>("Because there aren't 99 legends")
//
// [<Fact>]
// let ``Post method should add a new Legend`` () =
//     // Arrange
//     let controller = LegendsController()
//     controller.SetConnectionString(connString)
//     let newLegend = Legend(0, "New Legend", "New Armor", "New Weapon", "New Race", "New Spec", "New Pet", 1)
//
//     // Act
//     let result = controller.Post(newLegend)
//
//     // Assert
//     result.Should().BeOfType<OkObjectResult>("Because the result was Ok")
//
//     // Verify that the new legend was actually added to the database
//     let conn = controller.GetConnection()
//     let query = "SELECT * FROM Legends WHERE name = @name"
//     let addedLegend = conn.QuerySingleOrDefault<Legend>(query, {| name = "New Legend" |})
//     addedLegend.Should().NotBeNull("Because it should be inserted")
//     addedLegend.Name.Should().Be("New Legend", "Because it should be inserted")
//
// [<Fact>]
// let ``Put method should update an existing Legend`` () =
//     // Arrange
//     let controller = LegendsController()
//     controller.SetConnectionString(connString)
//     let updatedLegend = Legend(1, "Updated Legend", "Updated Armor", "Updated Weapon", "Updated Race", "Updated Spec", "Updated Pet", 2)
//     System.Threading.Thread.Sleep(2500)
//
//     let conn = controller.GetConnection()
//     let query = "SELECT * FROM Legends WHERE name = @name"
//     let legend = conn.QuerySingleOrDefault<Legend>(query, {| name = "New Legend" |})
//     
//     
//     // Act
//     let result = controller.Put(legend.Id, updatedLegend)
//
//     // Assert
//     result.Should().BeOfType<NoContentResult>("Because it doesn't return a content")
//
//     // Verify that the legend was actually updated in the database
//     let query = "SELECT * FROM Legends WHERE id = @id"
//     let updatedLegendFromDb = conn.QuerySingleOrDefault<Legend>(query, {| id = legend.Id |})
//     updatedLegendFromDb.Should().NotBeNull("Because it should still be in DB")
//     updatedLegendFromDb.Name.Should().Be("Updated Legend", "Because it was changed")
//
// [<Fact>]
// let ``Put method should return NotFound for non-existent Legend`` () =
//     // Arrange
//     let controller = LegendsController()
//     controller.SetConnectionString(connString)
//     let updatedLegend = Legend(99, "Updated Legend", "Updated Armor", "Updated Weapon", "Updated Race", "Updated Spec", "Updated Pet", 2)
//
//     // Act
//     let result = controller.Put(99, updatedLegend)
//
//     // Assert
//     result.Should().BeOfType<NotFoundObjectResult>("Because there aren't 99 Legends")
//
// [<Fact>]
// let ``Delete method should delete an existing Legend`` () =
//     // Arrange
//     let controller = LegendsController()
//     controller.SetConnectionString(connString)
//
//     System.Threading.Thread.Sleep(3000)
//     
//     let conn = controller.GetConnection()
//     let query = "SELECT * FROM Legends WHERE name = @name"
//     let legend = conn.QuerySingleOrDefault<Legend>(query, {| name = "Updated Legend" |})
//     
//     
//     
//     // Act
//     let result = controller.Delete(legend.Id)
//
//     // Assert
//     result.Should().BeOfType<NoContentResult>("Because it was OK with no content")
//
//     // Verify that the legend was actually deleted from the database
//     let conn = controller.GetConnection()
//     let query = "SELECT * FROM Legends WHERE id = @id"
//     let deletedLegend = conn.QuerySingleOrDefault<Legend>(query, {| id= legend.Id |})
//     deletedLegend.Should().BeNull("Because it should be deleted")
//
// [<Fact>]
// let ``Delete method should return NotFound for non-existent Legend`` () =
//     // Arrange
//     let controller = LegendsController()
//     controller.SetConnectionString(connString)
//
//     // Act
//     let result = controller.Delete(99)
//
//     // Assert
//     result.Should().BeOfType<NotFoundObjectResult>("Because there aren't 99 Legends")
    
    
let [<EntryPoint>] main _ = 0