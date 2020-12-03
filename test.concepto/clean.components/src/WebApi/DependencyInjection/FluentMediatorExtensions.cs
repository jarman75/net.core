using Microsoft.Extensions.DependencyInjection;
using FluentMediator;
using Application.UseCases.GetUserDetails;
using Application.UseCases.CreateUser;
using Application.UseCases.UpdateUser;

namespace WebApi.DependencyInjection
{
    public static class FluentMediatorExtensions
    {
        public static IServiceCollection AddMediator(this IServiceCollection services)
        {
            services.AddFluentMediator(
                builder =>
                {                    

                    builder.On<GetUserDetailsInput>().PipelineAsync()
                        .Call<Application.UseCases.GetUserDetails.IUseCase>((handler, request) => handler.Execute(request));

                    builder.On<CreateUserInput>().PipelineAsync()
                        .Call<Application.UseCases.CreateUser.IUseCase>((handler, request) => handler.Execute(request));

                    builder.On<UpdateUserInput>().PipelineAsync()
                        .Call<Application.UseCases.UpdateUser.IUseCase>((handler, request) => handler.Execute(request));

                });

            return services;
        }

    }
}
