using Fiap.Health.Med.User.Manager.Api;
using Fiap.Health.Med.User.Manager.CrossCutting;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        // Add services to the container.

        // builder.Services.AddControllers();


        var startup = new Startup(builder.Configuration);
        startup.ConfigureServices(builder.Services);
        builder.Services.Migrations(builder.Configuration);

        var app = builder.Build();

        startup.Configure(app, app.Environment);
        
        app.MapControllers();
        app.Run();
    }
}
