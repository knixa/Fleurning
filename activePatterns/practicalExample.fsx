type Score = int * int

let (|CorrectScore|_|) ((expected: Score, actual: Score)) =
    if expected = actual then Some() else None

let (|Draw|HomeWin|AwayWin|) (score: Score) =
    match score with
    | h, a when h = a -> Draw
    | h, a when h > a -> HomeWin
    | _ -> AwayWin

let (|CorrectResult|_|) (expected: Score, actual: Score) =
    match (expected, actual) with
    | Draw, Draw -> Some()
    | HomeWin, HomeWin -> Some()
    | AwayWin, AwayWin -> Some()
    | _ -> None

let goalScore (expected: Score) (actual: Score) =
    let home = [ fst expected; fst actual ] |> List.min
    let away = [ snd expected; snd actual ] |> List.min
    (home * 15) + (away * 20)

let resultScore (expected: Score) (actual: Score) =
    match (expected, actual) with
    | CorrectScore -> 400
    | CorrectResult -> 100
    | _ -> 0


let calculatePoints (expected: Score) (actual: Score) =
    [ resultScore; goalScore ] |> List.sumBy (fun f -> f expected actual)


let assertNoScoreDrawCorrect = calculatePoints (0, 0) (0, 0) = 400
let assertHomeWinExactMatch = calculatePoints (3, 2) (3, 2) = 485
let assertHomeWin = calculatePoints (5, 1) (4, 3) = 180
let assertIncorrect = calculatePoints (2, 1) (0, 7) = 20
let assertDraw = calculatePoints (2, 2) (3, 3) = 170
