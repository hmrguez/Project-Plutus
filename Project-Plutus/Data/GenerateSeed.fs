module Project_Plutus.Data.GenerateSeed

open System
open Project_Plutus.Data.DataLists

let weightedRandom (items: ('a * int) list) =
    let rand = Random()
    let totalWeight = items |> List.sumBy snd
    let randValue = rand.Next(totalWeight)
    let rec loop acc = function
        | [] -> failwith "Invalid weights"
        | (item, weight) :: xs ->
            let acc' = acc + int weight
            if acc' > randValue then item
            else loop acc' xs
    loop 0 items
    
    
let private armorItems = (List.map (fun x -> (x, 4)) commonArmorList) @
                         (List.map (fun x -> (x, 2)) rareArmorList) @
                         (List.map (fun x -> (x, 1)) legendaryArmorList)    
    
let private weaponItems = (List.map (fun x -> (x, 4)) commonWeaponList) @
                          (List.map (fun x -> (x, 2)) rareWeaponList) @
                          (List.map (fun x -> (x, 1)) legendaryWeaponList)    
    
let private raceItems =  (List.map (fun x -> (x, 4)) commonRaceList) @
                         (List.map (fun x -> (x, 2)) rareRaceList) @
                         (List.map (fun x -> (x, 1)) legendaryRaceList)    
    
let private specItems =  (List.map (fun x -> (x, 4)) commonSpecializationList) @
                         (List.map (fun x -> (x, 2)) rareSpecializationList) @
                         (List.map (fun x -> (x, 1)) legendarySpecializationList)    
    
let private petItems = (List.map (fun x -> (x, 4)) commonPetList) @
                       (List.map (fun x -> (x, 2)) rarePetList) @
                       (List.map (fun x -> (x, 1)) legendaryPetList)

let seed (property: string) =
    match property with
    | "armor" -> weightedRandom armorItems
    | "weapon" -> weightedRandom weaponItems
    | "race" -> weightedRandom raceItems
    | "spec" -> weightedRandom specItems
    | "pet" -> weightedRandom petItems
    | _ -> failwith "Not correct argument"