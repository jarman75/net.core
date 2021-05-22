using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Service.Api.Publishers;
using Service.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessageController : ControllerBase
    {
        

        private readonly ILogger<MessageController> _logger;
        private readonly MessagePublisher _publisher;

        public MessageController(ILogger<MessageController> logger, MessagePublisher publisher)
        {
            _logger = logger;
            _publisher = publisher;
        }

        [HttpGet]
        public string Get()
        {
            return "Service online";
        }
        [HttpPost]
        public IActionResult AddMessage(Message message)
        {
            
            _publisher.SendMessage(message);
            
            _logger.LogInformation("Publish message");

            return new OkResult();
        }
    }
}
