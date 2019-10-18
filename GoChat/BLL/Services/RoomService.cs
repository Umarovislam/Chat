using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using Chat.Web.Models;
using GoChat.DAL.Entities;
using GoChat.DAL.Interfaces;

namespace BLL.Services
{
    public class RoomService : IRoomServices
    {
        private IUnitOfWork db { get; set; }

        public RoomService(IUnitOfWork ui)
        {
            this.db = ui;
        }

        public void CreateRoom(RoomDto room)
        {
            var user = db.Rooms.getById(room.UserAccount.Id);
            if (user != null)
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<RoomDto, Room>()).CreateMapper();
                var rooms = mapper.Map<RoomDto, Room>(room);
                db.Rooms.Create(rooms);
            }
        }

        public RoomDto GetRoom(string id)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Room, RoomDto>()).CreateMapper();
            return mapper.Map<Room, RoomDto>(db.Rooms.getById(id));
        }

        public IEnumerable<RoomDto> GetRooms(string Userid)
        {
            List<RoomDto> rooms = new List<RoomDto>();
            var user = db.Users.getById(Userid);
            if (user != null)
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Room, RoomDto>()).CreateMapper();
                rooms = mapper.Map<IEnumerable<Room>, List<RoomDto>>(db.Rooms.Find(u => u.UserAccount == user));
            }

            return rooms;
        }


        public void DeleteRoom(string id)
        {

            db.Rooms.Delete(id);
        }

        public void Dispose()
        {
            db.Dispose();;
        }
    }
}
