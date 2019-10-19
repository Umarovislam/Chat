using System;
using System.Collections.Generic;
using System.Text;
using BLL.DTO;

namespace BLL.Interfaces
{
    public interface IUserService
    {
        void CreateUser(UserDto user);
        UserDto GetUserProfile(string id);
        void UpdateUser(UserDto userDto);
        void Dispose();
    }

}
