using Fiap.Health.Med.User.Manager.Api;
using Fiap.Health.Med.User.Manager.CrossCutting;
using Microsoft.Data.SqlClient;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var startup = new Startup(builder.Configuration);
        startup.ConfigureServices(builder.Services);
        builder.Services.Migrations(builder.Configuration);

        builder.Services.AddSingleton(new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Hackaton v1");
            });
        }

        app.UseRouting();
        app.UseAuthorization();
        app.MapControllers();
        app.UseHttpsRedirection();

        app.Run();
    }
}

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
