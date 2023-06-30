using MessageBrokerDomain.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBrokerDomain.Interfaces
{
    public interface IMessageHandler<TMessage> where TMessage : Message
    {
        Task Handle(TMessage message);
    }
}
