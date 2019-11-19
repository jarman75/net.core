using Microsoft.Extensions.DependencyInjection;

namespace WebApi.DependencyInjection
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            services.AddScoped<Application.UseCases.GetUserDetails.IUseCase, Application.UseCases.GetUserDetails.GetUserDetails>();
            services.AddScoped<Application.UseCases.CreateUser.IUseCase, Application.UseCases.CreateUser.CreateUser>();
            services.AddScoped<Application.UseCases.UpdateUser.IUseCase, Application.UseCases.UpdateUser.UpdateUser>();

            return services;
        }
    }
}
