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
    public class CalculationRequestCommandHandler : IRequestHandler<CalculationRequestCommand, CalculationResponseMessage>
    {
        private readonly IMessageBroker _messageBroker;
        public CalculationRequestCommandHandler(IMessageBroker messageBroker)
        {
            _messageBroker = messageBroker;
        }

        public async Task<CalculationResponseMessage> Handle(CalculationRequestCommand request, CancellationToken cancellationToken)
        {
            Console.Write($" client command i publish {request.Number1},  {request.Number2}\n");
                var kati =  await _messageBroker.PublishRPC(new CalculationRequestMessage(request.ClientsID,
                request.ClientsIP, request.Number1,
                request.Number2, request.ServiceName), "CalculationRequestQueue", "CalculationRequestReplyQueue");

            var ela = JsonConvert.DeserializeObject<CalculationResponseMessage>(kati);


            return ela;
        }


















        //public async Task<CalculationRequestMessage> Handle(CalculationRequestCommand request, CancellationToken cancellationToken)
        //{
        //    Console.Write($" client command i publish {request.Number1},  {request.Number2}\n");
        //    await _messageBroker.PublishRPC(new CalculationRequestMessage(request.ClientsID,
        //        request.ClientsIP,request.Number1,
        //        request.Number2,request.ServiceName),"CalculationRequestQueue","CalculationRequestReplyQueue");


        //    return new CalculationRequestMessage();
        //}

        //Task<CalculationResponseMessage> IRequestHandler<CalculationRequestCommand, CalculationResponseMessage>.Handle(CalculationRequestCommand request, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
