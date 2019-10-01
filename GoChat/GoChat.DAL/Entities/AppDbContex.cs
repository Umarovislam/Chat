using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore;


namespace GoChat.DAL.Entities
{
    public class AppDbContex : DbContext
    {
        public DbSet<AppUser> Users { get; set; }
        public DbSet<Friends> Friends { get; set; }
        public DbSet<Chat>Chats { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;user id=root;Password=UmarovIslam0898;database=gochatdb");
        }
    }
}
