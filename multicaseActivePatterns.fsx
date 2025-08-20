type Rank =
    | Ace
    | Two
    | Three
    | Four
    | Five
    | Six
    | Seven
    | Eight
    | Nine
    | Ten
    | Jack
    | Queen
    | King

type Suit =
    | Hearts
    | Clubs
    | Diamonds
    | Spades

type Card = Rank * Suit

let (|Red|Black|) (card: Card) =
    match card with
    | _, Diamonds
    | _, Hearts -> Red
    | _, Clubs
    | _, Spades -> Black

let describeColor card =
    match card with
    | Red -> "red"
    | Black -> "black"
    |> printfn "The card is %s"
