using System;
using System.Collections.Generic;
using System.Text;
using Chat.Web.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore;


namespace GoChat.DAL.Entities
{
    public class AppDbContex : IdentityDbContext<AppUser>
    {
        public DbSet<Room> Rooms { get; set; }

        public DbSet<Message> Messages { get; set; }
        public AppDbContex()
            : base()
        { }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;user id=root;Password=UmarovIslam0898;database=gochatdb");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
