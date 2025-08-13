open System

let tryParseDateTime (input: string) =
    match DateTime.TryParse input with
    | true, result -> Some result
    | _ -> None

type PersonName =
    { FirstName: string
      MiddleName: string option
      LastName: string }


let nullObj: string = null
let nullPri = Nullable<int>()

let fromNullObj = Option.ofObj nullObj
let fromNullPri = Option.ofNullable nullPri

let toNullObj = Option.toObj fromNullObj
let toNullPri = Option.toNullable fromNullPri

let defaultStringVal = Option.defaultValue "-----"
let resultFP = fromNullObj |> defaultStringVal
