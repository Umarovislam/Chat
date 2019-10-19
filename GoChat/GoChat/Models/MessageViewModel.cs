using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GoChat.Entities;

namespace Chat.Web.Models.ViewModels
{
    public class MessageViewModel
    {
        public string Id { get; set; }
        public string Content { get; set; }
        public string Timestamp { get; set; }
        public virtual ApplicationUser FromUser { get; set; }

        public virtual RoomViewModel ToRoom { get; set; }
    }
}