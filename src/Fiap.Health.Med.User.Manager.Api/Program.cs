using Fiap.Health.Med.User.Manager.Api.Extensions;
using Fiap.Health.Med.User.Manager.CrossCutting;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Data.SqlClient;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.ConfigureServices(builder.Configuration);
        builder.Services.Migrations(builder.Configuration);
        builder.Services.AddSingleton(new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));
        builder.Services.AddHealthChecks().AddSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), tags: new[] { "ready" });

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.MapHealthChecks("/health");
        app.MapHealthChecks("/health/live", new HealthCheckOptions()
        {
            Predicate = _ => true
        });
        app.MapHealthChecks("/health/ready", new HealthCheckOptions()
        {
            Predicate = check => check.Tags.Contains("ready")
        });
        app.UseCors();
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}