using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace GoChat.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string Avatar { get; set; }
    }
    public class UserInfo
    {
        public string Name;
        public string Email ;
        public string UserName;
        public string PhoneNumber;
        public string Avatar;
    }
}
