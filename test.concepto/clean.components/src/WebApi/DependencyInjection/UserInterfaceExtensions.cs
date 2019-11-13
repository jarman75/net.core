using Microsoft.Extensions.DependencyInjection;
using WebApi.UseCases.GetUserDetails;

namespace WebApi.DependencyInjection
{
    public static class UserInterfaceExtensions
    {
        
        public static IServiceCollection AddPresenters(this IServiceCollection services)
        {
            services.AddScoped<GetUserDetailsPresenter, GetUserDetailsPresenter>();
            services.AddScoped<Application.Boundaries.GetUserDetails.IOutputPort>(x => x.GetRequiredService<GetUserDetailsPresenter>());

            return services;
        }
    }
}
