using System;
using System.Collections.Generic;
using System.Text;

namespace GoChat.DAL.Entities
{
    public class Chat
    {
        public string Id { get; set; }
        public AppUser UserId { get; set;}
        public ICollection<AppUser>FriendsInChat { get; set; }
        public ICollection<string> Messages { get; set; }
    }
}
