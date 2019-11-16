using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentMediator;
using Application.Boundaries.GetUserDetails;
using Application.Boundaries.CreateUser;
using Application.Boundaries.UpdateUser;

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
                        .Call<Application.Boundaries.GetUserDetails.IUseCase>((handler, request) => handler.Execute(request));

                    builder.On<CreateUserInput>().PipelineAsync()
                        .Call<Application.Boundaries.CreateUser.IUseCase>((handler, request) => handler.Execute(request));

                    builder.On<UpdateUserInput>().PipelineAsync()
                        .Call<Application.Boundaries.UpdateUser.IUseCase>((handler, request) => handler.Execute(request));

                });

            return services;
        }

    }
}
