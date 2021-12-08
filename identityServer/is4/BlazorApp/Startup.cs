using BlazorApp.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;
using Microsoft.IdentityModel.Tokens;

namespace BlazorApp
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

            #region authentication
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = "oidc";
            })
             .AddCookie("Cookies", options =>
             {
                 options.ExpireTimeSpan = TimeSpan.FromSeconds(10);
                 options.SlidingExpiration = false;
                 options.Cookie.Name = "blazor-cookie";
             })
            .AddOpenIdConnect("oidc", options =>
            {
                options.Authority = "https://localhost:5001";
                options.RequireHttpsMetadata = false;

                options.ClientId = "BlazorApp";
                options.ClientSecret = "49C1A7E1-0C79-4A89-A3D6-A37998FB86B0";

                // code flow + PKCE (PKCE is turned on by default)
                options.ResponseType = "code";
                options.UsePkce = true;

                // keeps id_token smaller
                options.SaveTokens = true;
                options.GetClaimsFromUserInfoEndpoint = true;
                
                

                //options.Events.OnRedirectToIdentityProvider = context =>
                //{
                //    context.ProtocolMessage.Prompt = "login";
                //    return Task.CompletedTask;
                //};

                //options.TokenValidationParameters = new TokenValidationParameters
                //{
                //    NameClaimType = "name",
                //    RoleClaimType = "role"
                //};

                options.Scope.Add("openid");
                options.Scope.Add("profile");                
                options.Scope.Add("api1");
                
            });

            //adds user and client access token management
            services.AddAccessTokenManagement(options =>
            {
                options.Client.DefaultClient.Scope = "api1";

            });
                    //    .ConfigureBackchannelHttpClient()   

                    //.AddTransientHttpErrorPolicy(policy => policy.WaitAndRetryAsync(new[]
                    //{
                    //    TimeSpan.FromSeconds(1),
                    //    TimeSpan.FromSeconds(2),
                    //    TimeSpan.FromSeconds(3)
                    //}))
                    ;



            services.AddControllersWithViews(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            });
            #endregion

            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSingleton<WeatherForecastService>();

            // registers HTTP client that uses the managed user access token
            services.AddUserAccessTokenHttpClient("api1", configureClient: client =>
            {
                client.BaseAddress = new Uri("https://localhost:44313");
            });

            services.AddHttpClient("api1", c =>
            {
                c.BaseAddress = new Uri("https://localhost:44313");
                c.DefaultRequestHeaders.Add("Accept", "application/json");
                c.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory-BlazorApp");                

            })
                .AddUserAccessTokenHandler();
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
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();
            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
   
}
