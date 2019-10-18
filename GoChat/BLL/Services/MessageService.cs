using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using GoChat.DAL.Entities;
using GoChat.DAL.Interfaces;

namespace BLL.Services
{
    public class MessageService : IMessageService
    {
        private IUnitOfWork db { get; set; }

        public MessageService(IUnitOfWork ui)
        {
            this.db = ui;
        }

        public void newMessage(MessageDto str)
        {
            var user = db.Users.getById(str.FromUser.Id);
            if (user != null)
            {
                Message message = new Message()
                {
                    Id = str.Id,
                    Content = str.Content,
                    Timestamp = DateTime.Now.ToString(),
                    FromUser = user,
                };
                db.Messages.Create(message);
                db.Save();
            }
        }

        public List<MessageDto> getMessages(string id)
        {
            List<MessageDto> messages = new List<MessageDto>();
            var user = db.Users.getById(id);
            if (user != null)
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Message, MessageDto>()).CreateMapper();
                messages = mapper.Map<IEnumerable<Message>,List<MessageDto>>(db.Messages.Find(u => u.FromUser == user));
            }

            return messages;
        }

        public void DeleteMessage(string id)
        {
            db.Messages.Delete(id);
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}