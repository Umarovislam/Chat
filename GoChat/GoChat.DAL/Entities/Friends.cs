using System;
using System.Collections.Generic;
using System.Text;

namespace GoChat.DAL.Entities
{
    public class Friends
    {
        public string Id { get; set; }
        public AppUser UserId { get; set; }
        public ICollection<AppUser> Friend { get; set; }
        public string who { get; set; }
    }
}
