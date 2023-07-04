using Database.DbModels;
using Database.Interfaces;
using MessageBrokerDomain.Interfaces;
using ServerAplication.Messages;
using ServerAplication.Services;
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
        private readonly IRepository<CalculationDbModel> _repository;
        public CalculationRequestMessageHandler(IMessageBroker messageBroker,
            GetRequiredService getRequiredService,
            IRepository<CalculationDbModel> repository)
        {
            _messageBroker = messageBroker;
            _getRequiredService = getRequiredService;
            _repository = repository;
        }
        public  async Task Handle(CalculationRequestMessage message, string? replyQueue, string? correlationId)
        {
            var service = _getRequiredService.GetService(message.ServiceName);

            Console.WriteLine($"I am server i got this {message.Number1} and {message.Number2} \n");

            var result =  service.CalculateAsync(message.Number1,message.Number2);

            Console.WriteLine($" and give back this : {result}");

            message.Result = result;

            await _repository.AddAsync(new CalculationDbModel(message.ClientsID,message.ClientsIP,
                message.Number1,message.Number2,message.Result,message.ServiceName));

            _messageBroker.Publish(message,replyQueue,correlationId);

            
        }
    }
}
