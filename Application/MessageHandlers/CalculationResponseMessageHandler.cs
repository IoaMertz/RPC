using Application.Messages;
using MessageBrokerDomain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Application.MessageHandlers
{
    public class CalculationResponseMessageHandler : IMessageHandler<CalculationResponseMessage>
    {
        public Task Handle(CalculationResponseMessage message)
        {
            Console.WriteLine(" Server gave me this :  " + message.Result);
            return Task.CompletedTask;
        }
    }
}
