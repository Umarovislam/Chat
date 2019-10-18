using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTO
{
    public class MessageDto
    {
        public string Id { get; set; }
        public string Content { get; set; }
        public string Timestamp { get; set; }
        public virtual UserDto FromUser { get; set; }

        public virtual RoomDto ToRoom { get; set; }
    }
}