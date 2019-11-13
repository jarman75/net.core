using Application.Boundaries.GetUserDetails;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.UseCases.GetUserDetails
{
    public sealed class GetUserDetailsPresenter : IOutputPort
    {
        public IActionResult ViewModel { get; private set; }

        public void NotFound(string message)
        {
            ViewModel = new NotFoundObjectResult(message);

        }

        public void Standard(GetUserDetailsOutput output)
        {
            var response = new GetUserDetailsResponse(output.UserId, output.UserName.ToString(), output.Email.ToString());

            ViewModel = new OkObjectResult(response);
        }
    }
}
