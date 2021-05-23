using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BooksApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {

        private readonly IBooksRepository _books;
        public BooksController(IBooksRepository books)
        {
            _books = books;
        }

        [HttpGet]
        public ActionResult<List<Book>> Get()
        {
            
            var data = _books.Get();
            return data;
        }
    }
}
