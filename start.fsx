module First =
    type RegisteredCustomer = { Id: string }

    type Customer =
        | Eligible of RegisteredCustomer
        | Registered of RegisteredCustomer
        | Guest of Id: string

    let calculateTotal customer spend =
        let discount =
            match customer with
            | Eligible _ when spend >= 100.0M -> spend * 0.1M
            | _ -> 0.0M

        spend - discount


module WorkShop =
    type Customer =
        { Id: int
          IsVip: bool
          Credit: decimal }

    let getPurchases customer =
        let purchases = if customer.Id % 2 = 0 then 120M else 50M
        customer, purchases

    let tryPromoteToVip purchases =
        let customer, amount = purchases

        if amount > 100M then
            { customer with IsVip = true }
        else
            customer

    let increaseCreditIfVip customer =
        let increase = if customer.IsVip then 100M else 0M

        { customer with
            Credit = customer.Credit + increase }

    let upgradeCustomerComposed = getPurchases >> tryPromoteToVip >> increaseCreditIfVip

    let upgradeCustomerPiped customer =
        customer |> getPurchases |> tryPromoteToVip |> increaseCreditIfVip

module Fun =
    open System
    let apply f x y = f x y
    let sum = apply (fun x y -> x + y) 1 4

    let rnd () =
        let rand = Random()
        fun () -> rand.Next(100)

    List.init 50 (fun _ -> rnd ())

module PartialApplication =
    type LogLevel =
        | Error
        | Warning
        | Info

    let log (level: LogLevel) message = printfn $"[%A{level}]: %s{message}"

    let logError = log Error
    let logWarning = log Warning
    let logInfo = log Info
