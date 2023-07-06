using MessageBrokerDomain.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerAplication.Messages
{
    public class UserValidationResponseMessage: Message
    {
        public string UserId { get; set; }

        public UserValidationResponseMessage(string userId)
        {
            UserId = userId;
        }
    }
}
