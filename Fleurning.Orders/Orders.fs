namespace Fleurning.Orders

type Item = { ProductId: int; Quantity: int }

type Order = { Id: int; Items: Item list }

module Domain =
    let recalculate items =
        items
        |> List.groupBy (fun i -> i.ProductId)
        |> List.map (fun (id, items) ->
            { ProductId = id
              Quantity = items |> List.sumBy (fun i -> i.Quantity) })
        |> List.sortBy (fun i -> i.ProductId)

    let addItem item order =
        let items = item :: order.Items |> recalculate
        { order with Items = items }


    let addItems newItems order =
        let items = newItems @ order.Items |> recalculate
        { order with Items = items }
