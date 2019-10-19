using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL.DTO;

namespace Chat.Web.Models.ViewModels
{
    public class UserViewModel
    {
        public string Name { get; set; }
        public string Avatar { get; set; }

        public virtual ICollection<RoomViewModel> Rooms { get; set; }

        public virtual ICollection<MessageViewModel> Messages { get; set; }
    }
}