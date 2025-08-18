module Fleurning.ConsoleTests.OrderTests

open Fleurning.Orders
open Fleurning.Orders.Domain
open Xunit

module ``Add item to order tests`` =
    [<Fact>]
    let ``when product does not exist in empty order`` () =
        let emptyOrder = { Id = 1; Items = [] }

        let expected =
            { Id = 1
              Items = [ { ProductId = 1; Quantity = 3 } ] }

        let actual = emptyOrder |> addItem { ProductId = 1; Quantity = 3 }
        Assert.Equal(expected, actual)

    [<Fact>]
    let ``when product does not exist in order`` () =
        let emptyOrder =
            { Id = 1
              Items = [ { ProductId = 1; Quantity = 1 } ] }

        let expected =
            { Id = 1
              Items = [ { ProductId = 1; Quantity = 1 }; { ProductId = 2; Quantity = 5 } ] }

        let actual = emptyOrder |> addItem { ProductId = 2; Quantity = 5 }
        Assert.Equal(expected, actual)

    [<Fact>]
    let ``when product exist in non empty order`` () =
        let emptyOrder =
            { Id = 1
              Items = [ { ProductId = 1; Quantity = 1 } ] }

        let expected =
            { Id = 1
              Items = [ { ProductId = 1; Quantity = 4 } ] }

        let actual = emptyOrder |> addItem { ProductId = 1; Quantity = 3 }

        Assert.Equal(expected, actual)

module ``add multiple items to an order`` =

    [<Fact>]
    let ``when new products added to empty order`` () =
        let emptyOrder = { Id = 1; Items = [] }
        let items = [ { ProductId = 1; Quantity = 4 }; { ProductId = 2; Quantity = 2 } ]

        let expected =
            { Id = 1
              Items = [ { ProductId = 1; Quantity = 4 }; { ProductId = 2; Quantity = 2 } ] }

        let actual = emptyOrder |> addItems items

        Assert.Equal(expected, actual)

    [<Fact>]
    let ``when new products and updated existing to order`` () =
        let emptyOrder =
            { Id = 1
              Items = [ { ProductId = 2; Quantity = 2 } ] }

        let items = [ { ProductId = 1; Quantity = 3 }; { ProductId = 2; Quantity = 2 } ]

        let expected =
            { Id = 1
              Items = [ { ProductId = 1; Quantity = 3 }; { ProductId = 2; Quantity = 4 } ] }

        let actual = emptyOrder |> addItems items

        Assert.Equal(expected, actual)


module ``Removing a product`` =
    [<Fact>]
    let ``when remove all items of existing productid`` () =
        let order =
            { Id = 1
              Items = [ { ProductId = 1; Quantity = 1 } ] }

        let expected = { Id = 1; Items = [] }
        let actual = order |> removeProduct 1
        Assert.Equal(expected, actual)

    [<Fact>]
    let ``should do nothing for non-existant productId`` () =
        let order =
            { Id = 1
              Items = [ { ProductId = 1; Quantity = 1 } ] }

        let expected =
            { Id = 1
              Items = [ { ProductId = 1; Quantity = 1 } ] }

        let actual = order |> removeProduct 2
        Assert.Equal(expected, actual)


module ``Reduce item quantity`` =

    [<Fact>]
    let ``reduce existing item quantity`` () =
        let order =
            { Id = 1
              Items = [ { ProductId = 1; Quantity = 5 } ] }

        let expected =
            { Id = 1
              Items = [ { ProductId = 1; Quantity = 2 } ] }

        let actual = order |> reduceItem 1 3
        Assert.Equal(expected, actual)

    [<Fact>]
    let ``reduce existing item and remove`` () =
        let order =
            { Id = 2
              Items = [ { ProductId = 1; Quantity = 5 } ] }

        let expected = { Id = 2; Items = [] }

        let actual = order |> reduceItem 1 5
        Assert.Equal(expected, actual)

    [<Fact>]
    let ``reduce item with no quantity for empty order`` () =
        let order = { Id = 3; Items = [] }
        let expected = { Id = 3; Items = [] }

        let actual = order |> reduceItem 1 5
        Assert.Equal(expected, actual)


module ``Empty an order of all items`` =

    [<Fact>]
    let ``order with existing item`` () =
        let order =
            { Id = 4
              Items = [ { ProductId = 2; Quantity = 4 } ] }

        let expected = { Id = 4; Items = [] }

        let actual = order |> clearItems
        Assert.Equal(expected, actual)

    [<Fact>]
    let ``order with existing items`` () =
        let order =
            { Id = 4
              Items = [ { ProductId = 2; Quantity = 4 }; { ProductId = 4; Quantity = 2 } ] }

        let expected = { Id = 4; Items = [] }

        let actual = order |> clearItems
        Assert.Equal(expected, actual)

    [<Fact>]
    let ``empty order is unchanged`` () =
        let order = { Id = 4; Items = [] }

        let expected = { Id = 4; Items = [] }

        let actual = order |> clearItems
        Assert.Equal(expected, actual)
