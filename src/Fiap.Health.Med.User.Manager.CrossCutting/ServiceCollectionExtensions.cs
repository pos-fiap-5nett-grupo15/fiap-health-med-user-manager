using Fiap.Health.Med.User.Manager.Application.Interfaces;
using Fiap.Health.Med.User.Manager.Application.Services;
using Fiap.Health.Med.User.Manager.Application.Validators;
using Fiap.Health.Med.User.Manager.Domain.Interfaces;
using Fiap.Health.Med.User.Manager.Domain.Models.Doctor;
using Fiap.Health.Med.User.Manager.Infrastructure.Repositories;
using Fiap.Health.Med.User.Manager.Infrastructure.UnitOfWork;
using FluentValidation;
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
    
    }
}