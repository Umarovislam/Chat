using System;
using System.Collections.Generic;
using System.Text;
using BLL.DTO;

namespace BLL.Interfaces
{
    interface IMessageService
    {
        void newMessage(MessageDto str);
        List<MessageDto> getMessages(string id);
        void DeleteMessage(string id);
        void Dispose();
    }
}
