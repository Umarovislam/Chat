using System;
using System.Collections.Generic;
using System.Text;
using Chat.Web.Models;
using GoChat.DAL.Entities;

namespace GoChat.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Message> Messages { get; }
        IRepository<Room> Rooms { get; }
        IRepository<AppUser> Users { get; }
        void Save();
    }
}
