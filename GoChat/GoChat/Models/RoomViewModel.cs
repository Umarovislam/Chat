using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GoChat.Entities;

namespace Chat.Web.Models.ViewModels
{
    public class RoomViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ApplicationUser UserAccount { get; set; }

        public virtual ICollection<MessageViewModel> Messages { get; set; }
    }
}