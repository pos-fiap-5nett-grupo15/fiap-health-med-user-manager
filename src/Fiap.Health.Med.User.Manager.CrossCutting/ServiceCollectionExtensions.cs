using Fiap.Health.Med.User.Manager.Application.Interfaces;
using Fiap.Health.Med.User.Manager.Application.Services;
using Fiap.Health.Med.User.Manager.Application.Validators;
using Fiap.Health.Med.User.Manager.Domain.Interfaces;
using Fiap.Health.Med.User.Manager.Domain.Models.Doctor;
using Fiap.Health.Med.User.Manager.Infrastructure.Migrations;
using Fiap.Health.Med.User.Manager.Infrastructure.Repositories;
using Fiap.Health.Med.User.Manager.Infrastructure.UnitOfWork;
using FluentMigrator.Runner;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fiap.Health.Med.User.Manager.CrossCutting
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDataServices(this IServiceCollection services)
        {
            services.AddScoped<IHealthDatabase, HealthDatabase>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IValidator<Doctor>, DoctorValidator>();

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IDoctorService, DoctorService>();
            services.AddScoped<IDoctorRepository, DoctorRepository>();
            return services;
        }
        
        public static IServiceCollection Migrations(this IServiceCollection services, IConfiguration configuration)
        {
            using (var serviceProvider = BuildFluentMigrationServiceProvider(services, configuration))
            using (var scope = serviceProvider.CreateScope())
            {
                var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
                runner.MigrateUp();
            }
            return services;
        }
        
        private static ServiceProvider BuildFluentMigrationServiceProvider(IServiceCollection sc, IConfiguration configuration)
        {
            var strConnection = configuration.GetConnectionString("DatabaseDllConnection");
            if (string.IsNullOrEmpty(strConnection))
                throw new InvalidOperationException("DatabaseDllConnection is not defined.");
            
            return  new ServiceCollection().AddFluentMigratorCore()
                .ConfigureRunner( rb => 
                    rb.AddSqlServer()
                        .WithGlobalConnectionString(strConnection)
                        .ScanIn(typeof(CreateDoctorsTable).Assembly).For.Migrations()
                )
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .BuildServiceProvider(false);
        }
    
    }
}