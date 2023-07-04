using MessageBrokerDomain.Interfaces;
using ServerAplication.Services;
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
        private readonly GetRequiredService _getRequiredService;
        public CalculationRequestMessageHandler(IMessageBroker messageBroker, GetRequiredService getRequiredService)
        {
            _messageBroker = messageBroker;
            _getRequiredService = getRequiredService;
        }
        public  Task Handle(CalculationRequestMessage message, string? replyQueue, string? correlationId)
        {
            var service = _getRequiredService.GetService(message.ServiceName);

            Console.WriteLine($"I am server i got this {message.Number1} and {message.Number2} \n");

            var number =  service.CalculateAsync(message.Number1,message.Number2);

            Console.WriteLine($" and give back this : {number}");

            message.Result = number;

            _messageBroker.Publish(message,replyQueue,correlationId);

            return Task.CompletedTask;
        }
    }
}
