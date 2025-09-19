namespace ComputationExpressions

module AsyncResultDemo =

    open System
    open FsToolkit.ErrorHandling

    type AuthError = | UserBannedOrSuspended

    type TokenError = BadThingHappened of string

    type LoginError =
        | InvalidUser
        | InvalidPwd
        | Unauthorized of AuthError
        | TokenErr of TokenError

    type AuthToken = AuthToken of Guid

    type UserStatus =
        | Active
        | Suspended
        | Banned


    type User =
        { Name: string
          Password: string
          Status: UserStatus }

    [<Literal>]
    let ValidPassword = "password"

    [<Literal>]
    let ValidUser = "isvalid"

    [<Literal>]
    let SuspendedUser = "issuspended"

    [<Literal>]
    let BannedUser = "isbanned"

    [<Literal>]
    let BadLuckUser = "hasbadluck"

    [<Literal>]
    let AuthErrorMessage = "Earth's core stopped spinning"


    let tryGetUser username =
        async {
            let user =
                { Name = username
                  Password = ValidPassword
                  Status = Active }

            return
                match username with
                | ValidUser -> Some user
                | SuspendedUser -> Some { user with Status = Suspended }
                | BannedUser -> Some { user with Status = Banned }
                | BadLuckUser -> Some user
                | _ -> None
        }
