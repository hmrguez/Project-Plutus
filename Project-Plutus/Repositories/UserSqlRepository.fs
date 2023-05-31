module Project_Plutus.Repositories.UserSqlRepository

open System.Data.SqlClient
open Dapper
open Microsoft.Data.SqlClient
open Project_Plutus.Models

type UserRepository(connectionString: string) =
    let connection = new SqlConnection(connectionString)

    member this.GetAll() =
        use conn = connection
        conn.Query<User>("SELECT id, name, coin FROM Users")

    member this.GetByName(name: string) =
        use conn = connection
        conn.QuerySingleOrDefault<User>("SELECT id, name, coin FROM Users WHERE name = @name", {| name  = name|})

    member this.Insert(user: User) =
        use conn = connection
        conn.Execute("INSERT INTO Users (name, coin) VALUES (@name, @coin)", user)

    member this.Delete(user: User) =
        use conn = connection
        conn.Execute("DELETE FROM Users WHERE id = @id", user)

    member this.Update(user: User) =
        use conn = connection
        conn.Execute("UPDATE Users SET name = @name, coin = @coin WHERE id = @id", user)