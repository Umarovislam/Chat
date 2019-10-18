using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Chat.Web.Models;
using GoChat.DAL.Entities;
using GoChat.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GoChat.DAL.Repositories
{
    public class RoomRepository : IRepository<Room>
    {
        private AppDbContex db;

        public RoomRepository(AppDbContex contex)
        {
            this.db = contex;
        }
        public IEnumerable<Room> getAll()
        {
            return this.db.Rooms;
        }

        public Room getById(string id)
        {
            return this.db.Rooms.Find(id);
        }

        public IEnumerable<Room> Find(Func<Room, bool> predicate)
        {
            return this.db.Rooms.Where(predicate).ToList();
        }

        public void Create(Room item)
        {
            this.db.Add(item);
        }

        public void Update(Room item)
        {
            this.db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(string id)
        {
            var x = this.db.Rooms.Find(id);
            if (x != null)
            {
                this.db.Rooms.Remove(x);
            }
        }
    }
}
