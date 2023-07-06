using Database.DbModels;
using Database.Interfaces;
using Database.Services;
using MessageBrokerDomain.Interfaces;
using ServerAplication.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerAplication.MessageHandlers
{
    public class UserValidationRequestMessageHandler : IReplyMessageHandler<UserValidationRequestMessage>
    {
        private readonly IMessageBroker _messageBroker;

        private readonly IRepository<CalculationDbModel> _repo;

        private readonly LogInService _logInService;

        public UserValidationRequestMessageHandler(IMessageBroker messageBroker, IRepository<CalculationDbModel> repo, LogInService logInService)
        {
            _messageBroker = messageBroker;
            _repo = repo;
            _logInService = logInService;
        }

        public async Task Handle(UserValidationRequestMessage message, string? replyQueue, string? correlationId)
        {
            var userId = await _logInService.ValidateUser(message.UserId);
            _messageBroker.Publish(new UserValidationResponseMessage(message.UserId),replyQueue,correlationId);
        }
    }
}
