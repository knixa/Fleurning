﻿type IFizzBuzz =
    abstract member Calculate: int -> string

type FizzBuzz(mapping) =
    let calculate n =
        mapping
        |> List.map (fun (v, s) -> if n % v = 0 then s else "")
        |> List.reduce (+)
        |> fun s -> if s = "" then string n else s

    interface IFizzBuzz with
        member _.Calculate(value) = calculate value

let doFizzBuzz range =
    let fizzBuzz = FizzBuzz([ (3, "Fizz"); (5, "Buzz") ]) :> IFizzBuzz
    range |> List.map fizzBuzz.Calculate

let output = doFizzBuzz [ 1..15 ]
