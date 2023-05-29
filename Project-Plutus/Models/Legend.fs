namespace Project_Plutus.Models

type public Legend(id: int, name: string, armor: string, weapon: string, race: string, specialization: string, pet: string, expLevel: int) =
        member val Id = id with get
        member val Name = name with get
        member val Armor = armor with get
        member val Weapon = weapon with get
        member val Race = race with get
        member val Specialization = specialization with get
        member val Pet = pet with get
        member val ExpLevel = expLevel with get
        