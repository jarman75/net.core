using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApi.DependencyInjection;
using WebApi.DependencyInjection.FeatureFlags;

namespace WebApi
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
            
            services.AddControllers().AddControllersAsServices();
            
            services.AddBusinessExceptionFilter();
            
            services.AddFeatureFlags(Configuration);
            //services.AddVersioning();            
            //services.AddSwagger();

            services.AddUseCases();
            services.AddSQLServerPersistence(Configuration);
            services.AddPresenters();
            services.AddMediator();

             services.AddCors(options =>
                {
                options.AddPolicy("AllowAllHeaders",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:5000")
                            .AllowAnyHeader();
                    });
                });

            
        }

        

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors("AllowAllHeaders");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            
        }
    }
}
