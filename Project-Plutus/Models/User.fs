namespace Project_Plutus.Models

type public User(name: string, pCoin: int) =
    member val Name = name with get
    member val PCoin = pCoin with get