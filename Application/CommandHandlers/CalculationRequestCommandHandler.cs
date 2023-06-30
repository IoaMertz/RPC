using Application.Commands;
using Application.Messages;
using MediatR;
using MessageBrokerDomain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CommandHandlers
{
    public class CalculationRequestCommandHandler : IRequestHandler<CalculationRequestCommand, bool>
    {
        private readonly IMessageBroker _messageBroker;
        public CalculationRequestCommandHandler(IMessageBroker messageBroker)
        {
            _messageBroker = messageBroker;
        }
        public Task<bool> Handle(CalculationRequestCommand request, CancellationToken cancellationToken)
        {
            _messageBroker.Publish(new CalculationRequestMessage(request.Number,request.ServiceName),"CalculationRequestQueue","CalculationRequestReplyQueue");

            return Task.FromResult(true);
        }
    }
}
