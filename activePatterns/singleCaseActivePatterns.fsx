open System

let (|CharacterCount|) (input: string) = input.Length

let (|ContainsNumber|) (input: string) =
    input |> Seq.filter Char.IsDigit |> Seq.length > 0

let (|IsValidPassword|) (input: string) =
    match input with
    | CharacterCount len when len < 8 -> (false, "Password must contain at least 8 characters")
    | ContainsNumber false -> (false, "Password must contain at least one digit")
    | _ -> (true, "")

let setPassword input =
    match input with
    | IsValidPassword(true, _) as pwd -> Ok pwd
    | IsValidPassword(false, reason) -> Error $"Password not set: {reason}"
