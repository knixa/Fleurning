open System.IO

type Customer =
    { CustomerId: string
      Email: string
      IsEligible: string
      IsRegistered: string
      DateRegistered: string
      Discount: string }

type DataReader = string -> Result<string seq, exn>

let parseLine (line: string) : Customer option =
    match line.Split("|") with
    | [| customerId; email; eligible; registered; dateRegistered; discount |] ->
        Some
            { CustomerId = customerId
              Email = email
              IsEligible = eligible
              IsRegistered = registered
              DateRegistered = dateRegistered
              Discount = discount }
    | _ -> None

let parse (data: string seq) =
    data |> Seq.skip 1 |> Seq.map parseLine |> Seq.choose id

let readFile: DataReader =
    fun path ->
        try
            File.ReadLines(path) |> Ok
        with ex ->
            Error ex

let output data =
    data |> Seq.iter (fun x -> printfn $"%A{x}")

let import (dataReader: DataReader) path =
    match path |> dataReader with
    | Ok data -> data |> parse |> output
    | Error ex -> printfn $"Error: %A{ex.Message}"

let importWithFileReader = import readFile

Path.Combine(__SOURCE_DIRECTORY__, "Resources", "customers.csv")
|> importWithFileReader
