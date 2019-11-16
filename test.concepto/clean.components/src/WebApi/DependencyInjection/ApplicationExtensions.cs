using Microsoft.Extensions.DependencyInjection;

namespace WebApi.DependencyInjection
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            services.AddScoped<Application.Boundaries.GetUserDetails.IUseCase, Application.UseCases.GetUserDetails>();
            services.AddScoped<Application.Boundaries.CreateUser.IUseCase, Application.UseCases.CreateUser>();
            services.AddScoped<Application.Boundaries.UpdateUser.IUseCase, Application.UseCases.UpdateUser>();

            return services;
        }
    }
}
