module UtilsControllerTests

open FluentAssertions
open Xunit
open Project_Plutus.Controllers

type UtilsControllerTests() =
    let controller = new UtilsController()

    [<Fact>]
    member __.``GetGuid returns a valid GUID``() =
        let result = controller.GetGuid()
        let guid = result.Value :?> System.Guid
        
        result.StatusCode.Should().Be(200, "because the GUID should be valid") |> ignore
        guid.ToString().Length.Should().Be(36, "Because the GUID should be valid")
        // System.Guid.TryParse(guid.ToString(), _) |> should equal true
        // System.Guid.TryParse(guid.ToString(), _) |> should equal true






