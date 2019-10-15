using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GoChat.DAL.Entities;
using GoChat.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GoChat.DAL.Repositories
{
    public class UserRepository : IRepository<AppUser>
    {
        private AppDbContex db;

        public UserRepository(AppDbContex contex)
        {
            this.db = contex; 
        }

        public IEnumerable<AppUser> getAll()
        {
            return this.db.Users.ToList<AppUser>();
        }

        public AppUser getById(int id)
        {
            return db.Users.Find(id);
        }

        public IEnumerable<AppUser> Find(Func<AppUser, bool> predicate)
        {
            return db.Users.Where(predicate).ToList();
        }

        public void Create(AppUser item)
        {
            db.Users.Add(item);
        }

        public void Update(AppUser item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var x = db.Users.Find(id);
            if (x != null)
            {
                db.Users.Remove(x);
            }
        }
    }
}
