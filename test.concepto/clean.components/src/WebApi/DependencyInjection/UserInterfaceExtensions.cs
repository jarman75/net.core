using Microsoft.Extensions.DependencyInjection;
using WebApi.UseCases.CreateUser;
using WebApi.UseCases.GetUserDetails;
using WebApi.UseCases.UpdateUser;

namespace WebApi.DependencyInjection
{
    public static class UserInterfaceExtensions
    {
       public static IServiceCollection AddPresenters(this IServiceCollection services)
        {
            services.AddScoped<GetUserDetailsPresenter, GetUserDetailsPresenter>();
            services.AddScoped<Application.UseCases.GetUserDetails.IOutputPort>(x => x.GetRequiredService<GetUserDetailsPresenter>());

            services.AddScoped<CreateUserPresenter, CreateUserPresenter>();
            services.AddScoped<Application.UseCases.CreateUser.IOutputPort>(x => x.GetRequiredService<CreateUserPresenter>());

            services.AddScoped<UpdateUserPresenter, UpdateUserPresenter>();
            services.AddScoped<Application.UseCases.UpdateUser.IOutputPort>(x => x.GetRequiredService<UpdateUserPresenter>());


            return services;
        }
    }
}
