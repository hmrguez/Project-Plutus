

namespace Project_Plutus.Repositories

open Dapper
open Microsoft.Data.SqlClient
open Project_Plutus.Interfaces
open Project_Plutus.Models

type UserSqlRepository() =
    
    let connectionString = "Server=localhost\SQLEXPRESS;Database=PlutusDB;Encrypt=False;Trusted_Connection=True"
    member this.GetConnection () =
            new SqlConnection(connectionString)
    
    interface IUserRepository with
        member this.GetAll() =
            use conn = this.GetConnection()
            conn.Query<User>("SELECT * FROM Users").AsList()

        member this.GetByName(name: string) =
            use conn = this.GetConnection()
            let query = "SELECT * FROM Users WHERE name = @name"
            
            let resultOpt =
                try
                    Some(conn.QueryFirst<User>(query, {| name = name |}))
                with ex ->
                    None

            resultOpt

        member this.Insert(user: User) =
            use conn = this.GetConnection()
            try
                conn.Execute("INSERT INTO Users (name) VALUES (@name)", user) > 0
            with ex ->
                false
                

        member this.Delete(name: string) =
            use conn = this.GetConnection()
            conn.Execute("DELETE FROM Users WHERE name = @name", {| name = name |}) > 0

        member this.Update(user: User) =
            use conn = this.GetConnection()
            conn.Execute("UPDATE Users SET pCoin = @pCoin WHERE name = @name", user) > 0