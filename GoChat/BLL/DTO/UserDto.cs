using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace BLL.DTO
{
    public class UserDto : IdentityUser
    {
        public string Name { get; set; }
        public string PictureUrl { get; set; }
    }
}
