using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoChat.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace GoChat.Controllers
{
    public class ChatController : Controller
    {
        private readonly IHubContext<ChatHub> _hubContext;
    }
}
