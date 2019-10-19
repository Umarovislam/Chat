using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BLL.Interfaces;
using Chat.Web.Models.ViewModels;
using GoChat.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace GoChat.Hubs
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class ChatHub : Hub
    {
        private IRoomService roomService;
        public readonly static List<UserViewModel> _Connections = new List<UserViewModel>();

        private readonly static List<RoomViewModel> _Rooms = new List<RoomViewModel>();

        private readonly static Dictionary<string, string> _ConnectionsMap = new Dictionary<string, string>();

        public async Task Send(string message)
        {
            await Clients.All.SendCoreAsync("Receive", message.Split());
        }

    }
}
