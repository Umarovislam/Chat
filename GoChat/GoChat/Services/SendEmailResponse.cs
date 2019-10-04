using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoChat.Services
{
    public class SendEmailResponse
    {
        public bool Successful => ErrorMessage == null;
        
        public string ErrorMessage { get; set; }
    }
}
