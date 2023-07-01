using MessageBrokerDomain.Interfaces;
using ServerDomain.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerDomain.MessageHandlers
{
    public class CalculationRequestMessageHandler : IMessageHandler<CalculationRequestMessage>
    {
        public Task Handle(CalculationRequestMessage message)
        {
            throw new NotImplementedException();
        }
    }
}
