using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoChat.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace GoChat.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ChatController : Controller
    {
        private readonly IHubContext<ChatHub> _hubContext;

        public ChatController(IHubContext<ChatHub> hub)
        {
            _hubContext = hub;
        }

        public string Post([FromBody] object[] msg)
        {
            string retMessage = string.Empty;

            try
            {
                _hubContext.Clients.All.SendCoreAsync("Receive",msg);
                retMessage = "Success";
            }
            catch (Exception e)
            {
                retMessage = e.ToString();
            }

            return retMessage;
        }
    }
}
