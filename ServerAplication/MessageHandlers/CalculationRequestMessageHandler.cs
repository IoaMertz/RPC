using MessageBrokerDomain.Interfaces;
using ServerDomain.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerAplication.MessageHandlers
{
    public class CalculationRequestMessageHandler : IReplyMessageHandler<CalculationRequestMessage>
    {
        private readonly IMessageBroker _messageBroker;
        public CalculationRequestMessageHandler(IMessageBroker messageBroker)
        {
            _messageBroker = messageBroker;
        }
        public Task Handle(CalculationRequestMessage message, string? replyQueue, string? correlationId)
        {
            Console.WriteLine("aaaaaaaaaaaaaaaaaaaa");
            message.Number = 00000000000000;

            _messageBroker.Publish(message,replyQueue,correlationId);

            return Task.CompletedTask;
        }
    }
}
