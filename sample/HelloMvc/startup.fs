namespace HelloMvc

open System
open Microsoft.AspNet.Builder
open Microsoft.Framework.DependencyInjection
open Microsoft.Framework.Logging
open Microsoft.AspNet.Hosting

type Startup(env: IHostingEnvironment) =
  do printfn "Env name: %s" env.EnvironmentName

  let configureServices (services: IServiceCollection) =
    printfn "Configuring services"
    services.AddMvc ()
    ()

  // Set up application services
  member public x.ConfigureServices (services: IServiceCollection) =
    configureServices services

  member public x.ConfigureDevelopmentServices (services: IServiceCollection) =
    configureServices services

  // Configure pipeline
  member public x.Configure (app: IApplicationBuilder, loggerFactory: ILoggerFactory) =
    //loggerFactory.AddConsole (fun (name, logLevel) -> true)
    System.Diagnostics.Debugger.Break () |> ignore
    app.UseErrorPage () |> ignore

    app.UseStaticFiles () |> ignore

    app.UseMvc (fun routes ->
      routes.MapRoute (name = "default", template = "{controller=Home}/{action=Index}/{id?}") |> ignore

      ()) |> ignore
