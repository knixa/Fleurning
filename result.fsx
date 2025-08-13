open System

let tryDivide (x: decimal) (y: decimal) =
    try
        Ok(x / y)
    with :? DivideByZeroException as ex ->
        Error ex
