using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using GoChat.DAL.Entities;

namespace Chat.Web.Models
{
    public class Room
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public virtual AppUser UserAccount { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
    }
}