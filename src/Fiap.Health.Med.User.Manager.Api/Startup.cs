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
            services.AddControllers();
            // services.AddDataServices();
            // services.AddServices();
            
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