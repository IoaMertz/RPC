using Application.Messages;
using MessageBrokerDomain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MessageHandlers
{
    public class UserValidationResponseMessageHandler : IMessageHandler<UserValidationResponseMessage>
    {
        public Task Handle(UserValidationResponseMessage message)
        {
            Console.WriteLine(message.);
            return Task.CompletedTask;
        }
    }
}
