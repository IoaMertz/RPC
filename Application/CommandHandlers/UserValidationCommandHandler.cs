using Application.Commands;
using Application.Messages;
using MediatR;
using MessageBrokerDomain.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CommandHandlers
{
    public class UserValidationCommandHandler : IRequestHandler<UserValidationCommand, string>
    {
        private readonly IMessageBroker _messageBroker;

        public UserValidationCommandHandler(IMessageBroker messageBroker)
        {
            _messageBroker = messageBroker;
        }
        public async Task<string> Handle(UserValidationCommand request, CancellationToken cancellationToken)
        {
            var responseMessage = await _messageBroker.PublishRPC(new UserValidationRequestMessage(request.UserId),"UserValidationRequestQueue","UserValidationReplyQueue");
            var validationResponse = JsonConvert.DeserializeObject<UserValidationResponseMessage>(responseMessage);
            return validationResponse.UserId;
        }
    }
}
