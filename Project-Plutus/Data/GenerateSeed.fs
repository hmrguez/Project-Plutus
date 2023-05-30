module Project_Plutus.Data.GenerateSeed

open Project_Plutus.Data.DataLists

let weightedRandom (items : seq<'a * float>) =
    let totalWeight = items |> Seq.sumBy snd
    let rand = System.Random()
    let rec select acc remaining =
        match remaining with
        | [] -> failwith "No items to select from"
        | [(item, _)] -> item
        | (item, weight)::xs ->
            let acc' = acc + weight
            if rand.NextDouble() * totalWeight <= acc' then item
            else select acc' xs
    select 0.0 (List.ofSeq items)
    
    
let private armorItems = (List.map (fun x -> (x, 4.0)) commonArmorList) @
                         (List.map (fun x -> (x, 2.0)) rareArmorList) @
                         (List.map (fun x -> (x, 1.0)) legendaryArmorList)    
    
let private weaponItems = (List.map (fun x -> (x, 4.0)) commonWeaponList) @
                          (List.map (fun x -> (x, 2.0)) rareWeaponList) @
                          (List.map (fun x -> (x, 1.0)) legendaryWeaponList)    
    
let private raceItems =  (List.map (fun x -> (x, 4.0)) commonRaceList) @
                         (List.map (fun x -> (x, 2.0)) rareRaceList) @
                         (List.map (fun x -> (x, 1.0)) legendaryRaceList)    
    
let private specItems =  (List.map (fun x -> (x, 4.0)) commonSpecializationList) @
                         (List.map (fun x -> (x, 2.0)) rareSpecializationList) @
                         (List.map (fun x -> (x, 1.0)) legendarySpecializationList)    
    
let private petItems = (List.map (fun x -> (x, 4.0)) commonPetList) @
                       (List.map (fun x -> (x, 2.0)) rarePetList) @
                       (List.map (fun x -> (x, 1.0)) legendaryPetList)

let seed (property: string) =
    match property with
    | "armor" -> weightedRandom armorItems
    | "weapon" -> weightedRandom weaponItems
    | "race" -> weightedRandom raceItems
    | "spec" -> weightedRandom specItems
    | "pet" -> weightedRandom petItems
    | _ -> failwith "Not correct argument"