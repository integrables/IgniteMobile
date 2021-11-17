using IgniteAdmin.Data;
using IgniteAdmin.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IgniteAdmin
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
           services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme,
                    options =>
                    {
                        options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                        options.SignOutScheme = OpenIdConnectDefaults.AuthenticationScheme;
                        options.Authority = Configuration["OIDC:Authority"];
                        options.ClientId = Configuration["OIDC:ClientId"];
                        options.ClientSecret = Configuration["OIDC:ClientSecret"];
                        options.ResponseType = "code";
                        options.Scope.Add("openid");
                        options.Scope.Add("profile");
                        options.Scope.Add("reports.generate");
                        //options.Scope.Add("roles");
                        // Save access and refresh tokens to authentication cookie.  the default is false
                        options.SaveTokens = true;
                        options.UsePkce = true;
                        // It's recommended to always get claims from the 
                        // UserInfoEndpoint during the flow. 
                        options.GetClaimsFromUserInfoEndpoint = true;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            //map claim to name for display on the upper right corner after login.  Can be name, email, etc.
                            NameClaimType = "name"
                        };

                        options.Events = new OpenIdConnectEvents
                        {
                            OnAccessDenied = context =>
                            {
                                context.HandleResponse();
                                context.Response.Redirect("/");
                                return Task.CompletedTask;
                            }
                        };
                    });

            services.AddControllersWithViews()
                .AddMicrosoftIdentityUI();

            services.AddAuthorization(options =>
            {
                // By default, all incoming requests will be authorized according to the default policy
                options.FallbackPolicy = options.DefaultPolicy;
            });

            services.AddHttpClient();

            services.AddRazorPages();
            services.AddServerSideBlazor()
                .AddMicrosoftIdentityConsentHandler();
            services.AddSingleton<WeatherForecastService>();

            services.AddScoped<AccessTokenStorage>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
