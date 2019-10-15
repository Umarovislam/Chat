using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GoChat.DAL.Entities;
using GoChat.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GoChat.DAL.Repositories
{
    public class MessageRepository : IRepository<Message>
    {
        private AppDbContex db;
        public MessageRepository(AppDbContex contex)
        {
            this.db = contex;
        }
        public IEnumerable<Message> getAll()
        {
            return db.Messages.ToList<Message>();
        }

        public Message getById(int id)
        {
            return this.db.Messages.Find(id);
        }

        public IEnumerable<Message> Find(Func<Message, bool> predicate)
        {
            return this.db.Messages.Where(predicate).ToList();
        }

        public void Create(Message item)
        {
            this.db.Messages.Add(item);
        }

        public void Update(Message item)
        {
            this.db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var x = db.Messages.Find(id);
            if (x != null)
            {
                this.db.Messages.Remove(x);
            }
        }
    }
}
