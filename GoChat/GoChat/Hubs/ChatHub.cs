using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using Chat.Web.Models.ViewModels;
using Microsoft.AspNetCore.SignalR;

namespace GoChat.Hubs
{
   // [Authorize(AuthenticationSchemes = "Bearer")]
    public class ChatHub : Hub
    {
        IRoomService roomService;

        IMessageService messageService;
        public readonly static List<UserViewModel> _Connections = new List<UserViewModel>();

        private readonly static List<RoomViewModel> _Rooms = new List<RoomViewModel>();

        private readonly static Dictionary<string, string> _ConnectionsMap = new Dictionary<string, string>();


        public ChatHub(IRoomService _room, IMessageService _message)
        {
            this.roomService = _room;
            this.messageService = _message;
        }
        public async Task Send(string message)
        {
            await Clients.All.SendCoreAsync("Receive",message.Split());
            SendToRoom(null,new MessageViewModel()
                {
                    Content = message
                }
            );
        }
        
        public void SendToRoom(string roomName, MessageViewModel message)
        {
            var mess = new MapperConfiguration(cfg => cfg.CreateMap<MessageViewModel, MessageDto>()).CreateMapper();
            var r = new MapperConfiguration(cfg => cfg.CreateMap<RoomViewModel,RoomDto>()).CreateMapper();
            try
            {
                var room = roomService.GetRoom(roomName);
                if (room == null)
                {
                    var newroom = new RoomViewModel()
                    {
                        Name = roomName
                    };
                    roomService.CreateRoom(r.Map<RoomViewModel,RoomDto>(newroom));
                }
                
                messageService.newMessage(mess.Map<MessageViewModel,MessageDto>(message));
//                Clients.Group(room.Name).SendCoreAsync(message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
