using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentMediator;
using Application.Boundaries.GetUserDetails;


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

                    
                });

            return services;
        }

    }
}
