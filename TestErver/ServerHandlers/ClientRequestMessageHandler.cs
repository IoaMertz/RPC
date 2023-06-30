using MessageBroker.Interfaces;
using RPCMessageBrokerClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestServer.ServerMessages;

namespace TestServer.ServerHandlers
{
    public class ClientRequestMessageHandler : IReplyMessageHandler<ClientRequestMessage>
    {
        

        public Task Handle(ClientRequestMessage message, string? replyQueue, string? correlationId)
        {
            Console.WriteLine($"{replyQueue} , {correlationId} ");
            var brooker = new RbMqMessageBroker();
            message.Number = 10000;

            brooker.Publish(message,replyQueue,correlationId);
            
            return Task.CompletedTask;
        }
    }
}
