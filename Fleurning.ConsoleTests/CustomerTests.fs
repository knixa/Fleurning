namespace CustomerTests

open System
open Xunit
open Fleurning.Console.Customer

module ``I can group my tests in a module and run`` =

    let customerVip = { Id = 1; IsVip = true; Credit = 0.0M }

    let customerStd =
        { Id = 2
          IsVip = false
          Credit = 100.0M }

    [<Fact>]
    let ``Should give vip more credit`` () =
        let expected =
            Ok(
                { Id = 1
                  IsVip = true
                  Credit = 100.0M }
            )

        let actual = upgradeCustomerComposed customerVip
        Assert.Equal(expected, actual)

    [<Fact>]
    let ```Should make eligible std to vip and increase credit`` () =
        let expected =
            Ok(
                { Id = 2
                  IsVip = true
                  Credit = 200.0M }
            )

        let actual = upgradeCustomerComposed customerStd
        Assert.Equal(expected, actual)

    [<Fact>]
    let ``Should not upgrade ineligible customer to vip`` () =
        let expected =
            Ok(
                { Id = 3
                  IsVip = false
                  Credit = 50.0M }
            )

        let actual =
            upgradeCustomerComposed
                { customerStd with
                    Id = 3
                    Credit = 50.0M }

        Assert.Equal(expected, actual)
        
