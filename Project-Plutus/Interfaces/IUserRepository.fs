module Project_Plutus.Interfaces.IUserRepository

open Project_Plutus.Models

type IUserRepository =
    abstract member GetAll : unit -> User list
    abstract member GetByName : string -> User option
    abstract member Insert : User -> unit
    abstract member Delete : User -> unit
    abstract member Update : User -> unit
