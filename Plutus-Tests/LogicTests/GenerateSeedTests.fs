// module GenerateSeedTests

// open Xunit
// open FluentAssertions
// open Project_Plutus.Data.GenerateSeed

// [<Collection("GenerateSeed Tests")>]

// type GenerateSeedTests() =
    
//     [<Fact>]
//     member this. ``weightedRandom should return an item from the given list based on its weight``() =
//         // Arrange
//         let items = [("item1", 1.0); ("item2", 2.0); ("item3", 3.0)]
//         let iterations = 1000000
//         let results = List.init iterations (fun _ -> weightedRandom items)

//         // Act
//         let item1Count = results |> Seq.filter ((=) "item1") |> Seq.length
//         let item2Count = results |> Seq.filter ((=) "item2") |> Seq.length
//         let item3Count = results |> Seq.filter ((=) "item3") |> Seq.length
        
//         // Assert
//         item1Count.Should().BeLessThan(item2Count, "Montecarlo isn't working") |> ignore
//         item2Count.Should().BeLessThan(item3Count, "Montecarlo isn't working")