using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTO
{
    public class RoomDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual UserDto UserAccount { get; set; }

        public virtual ICollection<MessageDto> Messages { get; set; }
    }
}
