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
    public class CalculationRequestMessageHandler : IMessageHandler<CalculationRequestMessage>
    {
        public Task Handle(CalculationRequestMessage message)
        {
            //Console.WriteLine(" Server gave me this :  " + message.Result);
            return Task.CompletedTask;
        }
    }
}
