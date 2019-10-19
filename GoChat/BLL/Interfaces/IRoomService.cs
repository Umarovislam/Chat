using System;
using System.Collections.Generic;
using System.Text;
using BLL.DTO;

namespace BLL.Interfaces
{
    public interface IRoomService
    {
        void CreateRoom(RoomDto room);
        RoomDto GetRoom(string id);
        IEnumerable<RoomDto> GetRooms(string Userid);
        void DeleteRoom(string id);
        void Dispose();
    }
}
