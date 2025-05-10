using Fiap.Health.Med.User.Manager.CrossCutting;
using Microsoft.OpenApi.Models;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IWebHostEnvironment;

namespace Fiap.Health.Med.User.Manager.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API Hackaton", Version = "v1" });
            });
            services.AddEndpointsApiExplorer();
            services.AddControllers();
            services.AddDataServices();
            services.AddServices();
            services.AddValidators();
            services.AddHealthChecks();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // app.UseSwagger();
                // app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();
        
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints((endpoints) => endpoints.MapControllers());
        }
    }
}