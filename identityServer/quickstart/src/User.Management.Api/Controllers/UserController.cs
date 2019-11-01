using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using User.Management.Api.Models;

namespace User.Management.Api.Controllers
{
    [Route("user")]   
    [Authorize(Roles = "TestRole")]
    public class UserController : ControllerBase
    {
        //private readonly UserManager<ApplicationUser> _userManager;

        public UserController() //UserManager<ApplicationUser> userManager)
        {
            //_userManager = userManager;
        }

        [HttpGet]
        public IActionResult Get(string userName)
        {
            var user = new Models.ApplicationUser {
                UserName = userName                
            };
            
            return new JsonResult(user);
        }
    }
}