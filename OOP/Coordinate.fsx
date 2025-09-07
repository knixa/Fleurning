open System

[<AllowNullLiteral>]
type Coordinate(latitude: float, longitude: float) =
    let equals (other: Coordinate) =
        if isNull other then
            false
        else
            latitude = other.Latitude && longitude = other.Longitude

    member _.Latitude = latitude
    member _.Longitude = longitude

    override this.GetHashCode() = hash (this.Latitude, this.Longitude)

    override this.Equals(obj) =
        match obj with
        | :? Coordinate as other -> equals other
        | _ -> false

    interface IEquatable<Coordinate> with
        member _.Equals(other: Coordinate) : bool = equals other

    static member op_Equality(this: Coordinate, other: Coordinate) = this.Equals(other)



let c1 = Coordinate(25.0, 11.98)
let c2 = Coordinate(25.0, 11.98)

let c3 = c1
c1 = c2
c1 = c3
