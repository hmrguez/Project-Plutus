namespace Project_Plutus.Controllers

open Microsoft.AspNetCore.Mvc
open Dapper
open Microsoft.Data.SqlClient


type Legend(id: int, name: string, armor: string, weapon: string, race: string, specialization: string, pet: string, expLevel: int) =
    member val Id = id with get
    member val Name = name with get
    member val Armor = armor with get
    member val Weapon = weapon with get
    member val Race = race with get
    member val Specialization = specialization with get
    member val Pet = pet with get
    member val ExpLevel = expLevel with get

[<ApiController>]
[<Route("[controller]")>]
type LegendsController() =
    inherit ControllerBase()

    let mutable connString = "Server=localhost\SQLEXPRESS;Database=PlutusDB;Encrypt=False;Trusted_Connection=True"

    member this.GetConnection () =
        new SqlConnection(connString)

    member this.SetConnectionString(newConnectionString: string) =
        connString <- newConnectionString

    [<HttpGet>]
    member this.Get() =
        use conn = this.GetConnection()
        conn.Query<Legend>("SELECT * FROM Legends")

     
    [<HttpGet("{id}")>]
    member this.GetById(id: int) =
        use conn = this.GetConnection()
        let query = "SELECT id, name, armor, weapon, race, specialization, pet, expLevel FROM Legends WHERE id = @id"
        let result = conn.QuerySingleOrDefault(query, {| id = id |})
        match result with
        | null -> NotFoundObjectResult() :> IActionResult
        | _ -> OkObjectResult(result) :> IActionResult

    [<HttpPost>]
    member this.Post(legend: Legend) =
        use conn = this.GetConnection()
        let query = "INSERT INTO Legends (name, armor, weapon, race, specialization, pet, expLevel) VALUES (@name, @armor, @weapon, @race, @specialization, @pet, @expLevel)"
        let q = conn.Execute(query, legend)
        OkObjectResult()

    [<HttpPut("{id}")>]
    member this.Put(id:int, [<FromBody>]legend: Legend) =
        use conn = this.GetConnection()
        let query = "UPDATE Legends SET name = @name, armor = @armor, weapon = @weapon, race = @race, specialization = @specialization, pet = @pet, expLevel = @expLevel WHERE id = @id"
        let updatedRows = conn.Execute(query, {| id = id; name = legend.Name; armor = legend.Armor; weapon = legend.Weapon; race = legend.Race; specialization = legend.Specialization; pet = legend.Pet; expLevel = legend.ExpLevel |})
        if updatedRows = 0 then NotFoundObjectResult() :> IActionResult
        else NoContentResult() :> IActionResult

    [<HttpDelete("{id}")>]
    member this.Delete(id: int) =
        use conn = this.GetConnection()
        let query = "DELETE FROM Legends WHERE id = @id"
        let deletedRows = conn.Execute(query, {|id = id|})
        if deletedRows = 0 then NotFoundObjectResult() :> IActionResult
        else NoContentResult() :> IActionResult