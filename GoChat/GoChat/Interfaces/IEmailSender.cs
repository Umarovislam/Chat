using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoChat.Services;

namespace GoChat.Interfaces
{
    public interface IEmailSender
    {
       Task<SendEmailResponse> SendEmailAsync(SendEmailDetails details);
    }
}
