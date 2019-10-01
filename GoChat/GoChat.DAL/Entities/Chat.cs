using System;
using System.Collections.Generic;
using System.Text;

namespace GoChat.DAL.Entities
{
    public class Chat
    {
        public string Id { get; set; }
        public PreChat Users { get; set; }
        public string Messages { get; set; }
    }
}
