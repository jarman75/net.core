using Application.UseCases.UpdateUser;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.UseCases.UpdateUser
{
    public sealed class UpdateUserPresenter : IOutputPort
    {
        public IActionResult ViewModel { get; private set; }
        
        public void NotFound(string message)
        {
            ViewModel = new NotFoundObjectResult(message);
        }

        public void Standard(UpdateUserOutput output)
        {
            var response = new UpdateUserResponse(output.UserId, output.UserName.ToString(), output.Email.ToString());            
            ViewModel = new OkObjectResult(response);
        }
    }
}
