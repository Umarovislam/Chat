using System;
using System.Collections.Generic;
using System.Text;

namespace GoChat.DAL.Entities
{
    public class PreChat
    {
        public int Id { get; set; }
        public ICollection<Chat> ChatId { get; set; }
        public ICollection<AppUser> User { get; set; }
    }
}
