namespace Project_Plutus.Models.Dtos

type public UserBuyLegendDto(userName: string, nftId: int, amount: int) =
    member val UserName = userName with get
    member val NftId = nftId with get
    member val Amount = amount with get

