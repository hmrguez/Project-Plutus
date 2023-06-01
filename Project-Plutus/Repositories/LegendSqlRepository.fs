namespace Project_Plutus.Repositories

open Microsoft.Data.SqlClient
open Project_Plutus.Interfaces
open Dapper
open Project_Plutus.Models

type LegendSqlRepository() =
    let mutable connString = "Server=localhost\SQLEXPRESS;Database=PlutusDB;Encrypt=False;Trusted_Connection=True"
    member this.GetConnection () =
            new SqlConnection(connString)
            
    interface ILegendRepository with
        member this.GetAllLegends() =
            use conn = this.GetConnection()
            conn.Query<Legend>("SELECT * FROM Legends").AsList()
            
        member this.GetLegendById(id) =
            use conn = this.GetConnection()
            let query = "SELECT * FROM Legends WHERE id = @id"
            
            let resultOpt =
                try
                    Some(conn.QueryFirst<Legend>(query, {| id = id |}))
                with ex ->
                    None

            resultOpt
                
            
        member this.RemoveLegendById(id) =
            use conn = this.GetConnection()
            let query = "DELETE FROM Legends WHERE id = @id"
            let deletedRows = conn.Execute(query, {|id = id|})
            deletedRows > 0
            
        member this.UpdateLegend(legend: Legend) =
            use conn = this.GetConnection()
            let query = "UPDATE Legends SET name = @name, armor = @armor, weapon = @weapon, race = @race, specialization = @specialization, pet = @pet, expLevel = @expLevel, owner = @owner, forSale = @forSale WHERE id = @id"
            let updatedRows = conn.Execute(query, {| id = legend.Id; name = legend.Name; armor = legend.Armor; weapon = legend.Weapon; race = legend.Race; specialization = legend.Specialization; pet = legend.Pet; expLevel = legend.ExpLevel; owner = legend.Owner; forSale = legend.ForSale |})
            updatedRows > 0
            
        member this.InsertLegend(legend) =
            use conn = this.GetConnection()
            let query = "INSERT INTO Legends (name, armor, weapon, race, specialization, pet, expLevel) VALUES (@name, @armor, @weapon, @race, @specialization, @pet, @expLevel)"
            conn.Execute(query, legend) |> ignore
            ()
        