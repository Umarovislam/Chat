using System;
using System.Collections.Generic;
using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using GoChat.DAL.Entities;
using GoChat.DAL.Interfaces;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private IUnitOfWork db { get; set; }

        public UserService(IUnitOfWork ui)
        {
            this.db = ui;
        }
        public void CreateUser(UserDto user)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<UserDto,AppUser>()).CreateMapper();
            var user1 = mapper.Map<UserDto, AppUser>(user);
            db.Users.Create(user1);
        }

        public UserDto GetUserProfile(string id)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<AppUser,UserDto>()).CreateMapper();
            return mapper.Map<AppUser, UserDto>(db.Users.getById(id));
        }

        public void UpdateUser(UserDto userDto)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<UserDto,AppUser>()).CreateMapper();
            AppUser user = mapper.Map<UserDto, AppUser>(userDto);
            db.Users.Update(user);
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
