using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdminApp.Pages
{
    public class LogoutModel : PageModel
    {
        public async void OnGetAsync()
        {
            await HttpContext.SignOutAsync("Cookies");
            var prop = new AuthenticationProperties()
            {
                RedirectUri = "/"
            };
            await HttpContext.SignOutAsync("oidc", prop);
        }
    }
}