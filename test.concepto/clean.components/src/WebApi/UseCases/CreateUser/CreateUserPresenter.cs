using Application.UseCases.CreateUser;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.UseCases.CreateUser
{
    public sealed class CreateUserPresenter : IOutputPort
    {
        public IActionResult ViewModel { get; private set; }

        public void Standard(CreateUserOutput output)
        {
            var response = new CreateUserResponse(output.UserId);
            ViewModel = new OkObjectResult(response);
        }
    }
}
