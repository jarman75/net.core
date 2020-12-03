using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Repositories;
using Domain.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace WebApi.UseCases.GetUsers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public object Get()
        {
            IEnumerable<User> data = _userRepository.Get();
            
            var count = data.Count();
            var queryString = Request.Query;
            if (queryString.Keys.Contains("$inlinecount"))
            {
                StringValues Skip;
                StringValues Take;
                int skip = (queryString.TryGetValue("$skip", out Skip)) ? Convert.ToInt32(Skip[0]) : 0;
                int top = (queryString.TryGetValue("$top", out Take)) ? Convert.ToInt32(Take[0]) : data.Count();
                return new { Items = data.Skip(skip).Take(top), Count = count };
            }
            else
            {
                return data.Select(s => new
                {
                    Id = s.Id.ToString(),
                    UserName = s.Name?.ToString() ?? string.Empty,
                    Email = s.Email?.ToString() ?? string.Empty,
                    Password = s.Password?.ToString() ?? string.Empty
                });
                 
            }
        }
    }
}