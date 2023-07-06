using Application.Commands;
using MediatR;
using MessageBrokerDomain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CommandHandlers
{
    public class UserValidationCommandHandler : IRequestHandler<UserValidationCommand, int>
    {
        private readonly IMessageBroker _messageBroker;

        public UserValidationCommandHandler(IMessageBroker messageBroker)
        {
            _messageBroker = messageBroker;
        }
        public Task<int> Handle(UserValidationCommand request, CancellationToken cancellationToken)
        {

            throw new NotImplementedException();
        }
    }
}
