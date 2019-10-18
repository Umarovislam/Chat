using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Chat.Web.Models;

namespace GoChat.DAL.Entities
{
    public class Message
    {
        public string Id { get; set; }
        public string Content { get; set; }
        public string Timestamp { get; set; }
        public virtual AppUser FromUser { get; set; }
        public virtual Room ToRoom { get; set; }
    }
}
