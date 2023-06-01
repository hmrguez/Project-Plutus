namespace Project_Plutus.Interfaces

open Project_Plutus.Models

type IUserRepository =
    abstract member GetAll : unit -> System.Collections.Generic.List<User>
    abstract member GetByName : string -> User option
    abstract member Insert : User -> bool
    abstract member Delete : string -> bool
    abstract member Update : User -> bool
