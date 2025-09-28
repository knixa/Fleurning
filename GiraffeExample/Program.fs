open Giraffe
open Giraffe.EndpointRouting
open Giraffe.ViewEngine
open GiraffeExample
open GiraffeExample.TodoStore
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Http
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting
open GiraffeExample.Shared

let indexView =
    [ h1 [] [ str "I |> F#" ]
      p [ _class "some-css-class"; _id "someId" ] [ str "Hello World from the Giraffe View Engine" ] ]
    |> masterPage "Giraffe View Engine Example"

let sayHelloNameHandler (name: string) : HttpHandler =
    fun (next: HttpFunc) (ctx: HttpContext) -> {| Response = $"Hello {name}, how are you?" |} |> ctx.WriteJsonAsync

let apiRoutes =
    [ GET
          [ route "" (json {| Response = "Hello World!!" |})
            routef "%s" sayHelloNameHandler ] ]


let endpoints =
    [ GET [ route "/" (htmlView (Todos.Views.todoView Todos.Data.todoList)) ]
      subRoute "/api" apiRoutes
      subRoute "/api/todo" Todos.apiTodoRoutes ]

let notFoundHandler = "Not Found" |> text |> RequestErrors.notFound

let configureApp (appBuilder: IApplicationBuilder) =
    appBuilder
        .UseRouting()
        .UseStaticFiles()
        .UseGiraffe(endpoints)
        .UseGiraffe(notFoundHandler)

let configureServices (services: IServiceCollection) =
    services.AddRouting().AddGiraffe().AddSingleton<TodoStore>(TodoStore())
    |> ignore

[<EntryPoint>]
let main args =
    let builder = WebApplication.CreateBuilder(args)
    configureServices builder.Services

    let app = builder.Build()

    if app.Environment.IsDevelopment() then
        app.UseDeveloperExceptionPage() |> ignore

    configureApp app

    app.Run()

    0
// Exit code
