using IdentitySuite.Resoureces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IdentitySuite
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
          
            services.AddIdentityServer()
                .AddInMemoryClients(StaticResources.GetClients())
                .AddInMemoryIdentityResources(StaticResources.GetIdentityResources())
                .AddInMemoryApiResources(StaticResources.GetApiResources())
                .AddInMemoryApiScopes(StaticResources.GetApiScopes())
                .AddTestUsers(StaticResources.GetTestUsers())
                .AddDeveloperSigningCredential();

            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
               app.UseDeveloperExceptionPage();
            }

            
            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors(
                options => options.SetIsOriginAllowed(x => _ = true).AllowAnyMethod().AllowAnyHeader().AllowCredentials()
            ); //This needs to set everything allowed

            app.UseIdentityServer();

            app.UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());
        }
    }
}