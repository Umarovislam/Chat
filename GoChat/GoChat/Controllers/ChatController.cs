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
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] object[] msg)
        {
            string retMessage = string.Empty;

            try
            {
                await _hubContext.Clients.All.SendCoreAsync("Receive",msg);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }
    }
}
