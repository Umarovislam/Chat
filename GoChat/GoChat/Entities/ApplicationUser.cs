using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace GoChat.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public long? GoogleId { get; set; }
        public string PictureUrl { get; set; }
    }
}
