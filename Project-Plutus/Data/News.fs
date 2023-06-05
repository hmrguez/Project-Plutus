namespace Project_Plutus.Data

type public News(author: string, title: string, description: string) =
    member val Author = author with get
    member val Title = title with get
    member val Description = description with get