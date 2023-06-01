module GenerateSeedTests

open Xunit
open FluentAssertions
open Project_Plutus.Data.GenerateSeed

[<Collection("GenerateSeed Tests")>]

type GenerateSeedTests() =
    
    [<Fact>]
    member this. ``Montecarlo for Weighted Random should be accurate``() =
        // Arrange
        let items = [("item1", 3); ("item2", 2); ("item3", 1)]
        let iterations = 1000000
        let results = List.init iterations (fun _ -> weightedRandom items)

        // Act
        let item1Count = results |> Seq.filter ((=) "item1") |> Seq.length
        let item2Count = results |> Seq.filter ((=) "item2") |> Seq.length
        let item3Count = results |> Seq.filter ((=) "item3") |> Seq.length
        
        
        // Assert
        item1Count.Should().BeGreaterThan(item2Count, "Montecarlo isn't working") |> ignore
        item2Count.Should().BeGreaterThan(item3Count, "Montecarlo isn't working")