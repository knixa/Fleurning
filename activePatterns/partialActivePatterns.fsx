open System

let (|ValidDate|_|) (input: string) =
    match DateTime.TryParse(input) with
    | true, value -> Some value
    | false, _ -> None

let parse input =
    match input with
    | ValidDate dt -> printf $"%A{dt}"
    | _ -> printfn $"%s{input} is not a valid date"

let (|IsDivisibleByList|_|) divisors n =
    if divisors |> List.forall (fun div -> n % div = 0) then
        Some()
    else
        None

let (|IsDivisibleBy|_|) divisor n =
    if n % divisor = 0 then Some() else None

let (|NotDivisibleBy|_|) divisor n =
    if n % divisor <> 0 then Some() else None

let calculate i =
    match i with
    | IsDivisibleByList [ 3; 5 ] -> "FizzBuzz"
    | IsDivisibleByList [ 3 ] -> "Fizz"
    | IsDivisibleByList [ 5 ] -> "Buzz"
    | _ -> i |> string

let calculateReduce mapping n =
    mapping
    |> List.map (fun (divisor, result) -> if n % divisor = 0 then result else "")
    |> List.reduce (+)
    |> fun input -> if input = "" then string n else input


let isLeapYear year =
    match year with
    | IsDivisibleBy 400 -> true
    | IsDivisibleBy 4 & NotDivisibleBy 100 -> true
    | _ -> false
