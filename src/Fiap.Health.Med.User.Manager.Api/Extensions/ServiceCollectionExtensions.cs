using Fiap.Health.Med.User.Manager.CrossCutting;
using Microsoft.OpenApi.Models;

namespace Fiap.Health.Med.User.Manager.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwagger();
            services.AddEndpointsApiExplorer();
            services.AddControllers();
            services.AddDataServices();
            services.AddServices();
            services.AddValidators();
            services.AddHealthChecks();
            return services;
        }

        private static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "FIAP Health & Med - User API",
                    Version = "v1",
                    Description = "FIAP Health & Med: User API - FIAP Students Project - Group 15"
                });
            });

            return services;
        }
    }
}
