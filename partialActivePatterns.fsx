open System

let (|ValidDate|_|) (input: string) =
    match DateTime.TryParse(input) with
    | true, value -> Some value
    | false, _ -> None

let parse input =
    match input with
    | ValidDate dt -> printf $"%A{dt}"
    | _ -> printfn $"%s{input} is not a valid date"

let (|IsDivisibleBy|_|) divisors n =
    if divisors |> List.forall (fun div -> n % div = 0) then
        Some()
    else
        None

let calculate i =
    match i with
    | IsDivisibleBy [ 3; 5 ] -> "FizzBuzz"
    | IsDivisibleBy [ 3 ] -> "Fizz"
    | IsDivisibleBy [ 5 ] -> "Buzz"
    | _ -> i |> string

let calculateReduce mapping n =
    mapping
    |> List.map(fun(divisor, result) -> if n % divisor = 0 then result else "")
    |> List.reduce(+)
    |> fun input -> if input = "" then string n else input