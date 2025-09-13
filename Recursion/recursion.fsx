// naive approach
let rec naiveFact n =
    match n with
    | 1 -> 1
    | n -> n * naiveFact (n - 1)

// tail call optimisation
let fact n =
    let rec loop n acc =
        match n with
        | 1 -> acc
        | _ -> loop (n - 1) (acc * n)

    loop n 1

// naive fib
let rec naiveFib (n: int64) =
    match n with
    | 0L -> 0L
    | 1L -> 1L
    | s -> naiveFib (s - 1L) + naiveFib (s - 2L)

// tail call optimisation
let fib (n: int64) =
    let rec loop n (a, b) =
        match n with
        | 0L -> a
        | 1L -> b
        | n -> loop (n - 1L) (b, a + b)

    loop n (0L, 1L)

let mapping = [(3, "Fizz"); (5, "Buzz"); (7,"Bazz")]

let fizzBuzzRec initialMapping n =
    let rec loop mapping acc =
        match mapping with
        | [] -> if acc = "" then string n else acc
        | head::tail ->
            let value =
                head |> (fun(div, msg) -> if n % div = 0 then msg else "")
            loop tail (acc + value)
    loop initialMapping ""
       
[1 .. 25]
|> List.map (fizzBuzzRec mapping)
|> List.iter (printfn "%s")

let fizzBuzzfold n =
    [(3,"Fizz"); (5, "Buzz")]
    |> List.fold (fun acc (div, msg) ->
        match (if n % div = 0 then msg else "") with
        | "" -> acc
        | s -> if acc = string n then s else acc + s)(string n)