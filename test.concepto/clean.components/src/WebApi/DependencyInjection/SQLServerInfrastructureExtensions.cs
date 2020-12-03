using Application.Repositories;
using Application.Services;
using Domain;
using Infrastructure;
using Infrastructure.EntityFrameworkDataAccess;
using Insfrastructure.EntityFrameworkDataAccess.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.DependencyInjection
{
    public static class SQLServerInfrastructureExtensions
    {
        public static IServiceCollection AddSQLServerPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IEntityFactory, EntityFactory>();

            services.AddDbContext<IdentityContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders();



            services.AddScoped<IUnitOfWork, UnitOfWork>();


            services.AddScoped<IUserRepository, UserRepository>();

            return services;

        }
    }
}
