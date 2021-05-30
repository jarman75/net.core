using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserService.Data;
using UserService.Publishers;

namespace UserService
{
    public class EventStartup
    {
        public EventStartup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<UserServiceContext>(options =>
                options.UseSqlite(@"Data Source=user.db"));

            services.AddSingleton<IntegrationEventSenderService>();
            services.AddHostedService<IntegrationEventSenderService>();
        }
        
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {            
                       
        }

    }
}
