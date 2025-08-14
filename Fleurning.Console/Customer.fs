namespace Fleurning.Console

module Customer =
    type Customer =
        { Id: int
          IsVip: bool
          Credit: decimal }

    let getPurchases customer =
        try
            let purchases =
                if customer.Id % 2 = 0 then
                    (customer, 120M)
                else
                    (customer, 80M)

            Ok purchases
        with ex ->
            Error ex

    let tryPromoteToVip purchases =
        let customer, amount = purchases

        if amount > 100M then
            { customer with IsVip = true }
        else
            customer

    let increaseCreditIfVip customer =
        try
            let increase = if customer.IsVip then 100M else 0M

            Ok
                { customer with
                    Credit = customer.Credit + increase }
        with ex ->
            Error ex

    let upgradeCustomerComposed =
        getPurchases >> Result.map tryPromoteToVip >> Result.bind increaseCreditIfVip

    let upgradeCustomerPiped customer =
        customer
        |> getPurchases
        |> Result.map tryPromoteToVip
        |> Result.bind increaseCreditIfVip
