using MessageBrokerDomain.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Messages
{
    public class UserValidationRequestMessage :Message
    {
        public string UserId { get; set; }

        public UserValidationRequestMessage(string userId)
        {
            UserId = userId;
        }
    }
}
