using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Chat.Web.Models;
using Microsoft.AspNetCore.Identity;

namespace GoChat.DAL.Entities
{
    public class AppUser : IdentityUser
    {
        public string PhotoLocation { get; set; }
        public string DisplayName { get; set; }

        public string Avatar { get; set; }

        public virtual ICollection<Room> Rooms { get; set; }

        public virtual ICollection<Message> Messages { get; set; }

    }
}
