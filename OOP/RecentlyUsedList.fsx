type IRecentlyUsedList =
    abstract member IsEmpty: bool
    abstract member Size: int
    abstract member Capacity: int
    abstract member Clear: unit -> unit
    abstract member Add: string -> unit
    abstract member TryGet: int -> string option

type RecentlyUsedList(capacity: int) =
    let items = ResizeArray<string>(capacity)

    let add item =
        items.Remove item |> ignore
        if items.Count = items.Capacity then items.RemoveAt 0
        items.Add item

    let get index =
        if index >= 0 && index < items.Count then
            Some items[items.Count - index - 1]
        else
            None

    interface IRecentlyUsedList with
        member _.IsEmpty = items.Count = 0
        member _.Size = items.Count
        member _.Capacity = items.Capacity
        member _.Clear() = items.Clear()
        member _.Add(item) = add item
        member _.TryGet(index) = get index

let rul = RecentlyUsedList(5) :> IRecentlyUsedList

rul.Capacity = 5
rul.Add "Test"

rul.IsEmpty = false
rul.Add "Test2"
rul.Add "Test3"
rul.Add "Test"
rul.Add "Test6"
rul.Add "Test7"
rul.Add "Test"
rul.Size = 5
rul.Capacity = 5

rul.TryGet(0) = Some "Test"
rul.TryGet(4) = Some "Test2"
