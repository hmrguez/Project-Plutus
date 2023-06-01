namespace Project_Plutus
#nowarn "20"
open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting
open Project_Plutus.Interfaces
open Project_Plutus.Repositories

module Program =
    let exitCode = 0

    [<EntryPoint>]
    let main args =

        let builder = WebApplication.CreateBuilder(args)

        builder.Services.AddControllers()
        builder.Services.AddSingleton<ILegendRepository, LegendSqlRepository>()
        builder.Services.AddSingleton<IUserRepository, UserSqlRepository>()
        
        let app = builder.Build()

        app.UseHttpsRedirection()
     
        
        app.UseAuthorization()
        app.MapControllers()

        app.Run()

        exitCode
