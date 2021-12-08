using System;
using System.Collections.Generic;
using IdentityModel.AspNetCore.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace webapi
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

            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;                
                
            });

            services.AddAuthentication("token")
                .AddJwtBearer("token", options =>
                {
                    options.Authority = "https://localhost:5001";

                    // if you are using API resources, you can specify the name here
                    options.Audience = "api1";                    

                    // IdentityServer emits a typ header by default, recommended extra check
                    options.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };

                    // if token does not contain a dot, it is a reference token
                    options.ForwardDefaultSelector = Selector.ForwardReferenceToken("introspection");

                })
                 // reference tokens
                 .AddOAuth2Introspection("introspection", options =>
                 {
                     options.Authority = "https://localhost:5001";

                     options.ClientId = "api1";
                     options.ClientSecret = "4HGYugUKMklfReduECoo6bBigov3pR/Y7URi9T4CBdk=";
                 });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "webapi", Version = "v1" });

                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        AuthorizationCode = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri("https://localhost:5001/connect/authorize"),
                            TokenUrl = new Uri("https://localhost:5001/connect/token"),
                            Scopes = new Dictionary<string, string>
                            {
                                {"api1", "WeatherForecast api"}
                            }
                        }
                    }
                });

                c.OperationFilter<AuthorizeCheckOperationFilter>();

            });

            




        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                
                app.UseSwagger();
                app.UseSwaggerUI(c => 
                    {
                        c.SwaggerEndpoint("/swagger/v1/swagger.json", "webapi v1");
                        c.OAuthClientId("swaggerUI");
                        c.OAuthAppName("swagger webapi");                        
                        c.OAuthUsePkce();
                    });
                
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
