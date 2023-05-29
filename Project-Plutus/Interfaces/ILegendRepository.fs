namespace Project_Plutus.Interfaces

open Project_Plutus.Models

type ILegendRepository =
    abstract member GetAllLegends : unit -> System.Collections.Generic.List<Legend>
    abstract member GetLegendById : id:int -> Legend option
    abstract member RemoveLegendById : id:int -> bool
    abstract member InsertLegend : legend:Legend -> unit
    abstract member UpdateLegend : legend:Legend -> bool
    