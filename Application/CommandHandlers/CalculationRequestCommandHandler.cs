﻿using Application.Commands;
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
        public async Task<bool> Handle(CalculationRequestCommand request, CancellationToken cancellationToken)
        {
            Console.WriteLine("aaaaaa");
            await _messageBroker.PublishRPC(new CalculationRequestMessage(request.Number,request.ServiceName),"CalculationRequestQueue","CalculationRequestReplyQueue");

            Console.WriteLine("I SEND rrequest number " + request.Number);

            return true;
        }
    }
}
