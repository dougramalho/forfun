using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MessageBoardBackend.Controllers
{
    [Produces("application/json")]
    [Route("api/Messages")]
    public class MessagesController : Controller
    {
        static List<Models.Message> messages = new List<Models.Message>()
            {
                new Models.Message()
                {
                    Owner = "John",
                    Text = "hello"
                },
                new Models.Message()
                {
                    Owner = "Tim",
                    Text = "Hi"
                }
            };

        public IEnumerable<Models.Message> Get()
        {
            return messages;
        }

        [HttpPost]
        public void Post([FromBody] Models.Message message)
        {
            messages.Add(message);
        }
    }
}
