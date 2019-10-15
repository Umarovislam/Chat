using System;
using System.Collections.Generic;
using System.Text;
using Chat.Web.Models;
using GoChat.DAL.Entities;
using GoChat.DAL.Interfaces;

namespace GoChat.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private bool disposed = false;
        private AppDbContex db;
        private MessageRepository messageRepository;
        private UserRepository userRepository;
        public EFUnitOfWork()
        {
            this.db = new AppDbContex();
        }

        public void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.db.Dispose();
                }
            }
        }

        public IRepository<Message> Messages
        {
            get
            {
                if(messageRepository == null)
                {
                    messageRepository = new MessageRepository(db);
                }

                return messageRepository;
            }
        }

        public IRepository<Room> Rooms { get; set; }
        public IRepository<AppUser> Users { get; set; }
        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
