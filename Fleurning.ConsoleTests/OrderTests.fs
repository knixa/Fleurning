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
        let emptyOrder = { Id = 1; Items = [{ProductId = 2; Quantity = 2 }] }
        let items = [ { ProductId = 1; Quantity = 3 }; { ProductId = 2; Quantity = 2 } ]

        let expected =
            { Id = 1
              Items = [ { ProductId = 1; Quantity = 3 }; { ProductId = 2; Quantity = 4 } ] }

        let actual = emptyOrder |> addItems items 

        Assert.Equal(expected, actual)
