using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace GoChat.DAL.Entities
{
    public class AppUser : IdentityUser
    {
        public ICollection<Chat> Chats { get; set; }
        public string PhotoLocation { get; set; }
        public ICollection<Friends> MyFriends{get; set; }
    }
}
