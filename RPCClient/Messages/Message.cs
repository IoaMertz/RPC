using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBroker.Messages
{
    public abstract class Message
    {
        public string MessageType { get; protected set; }
        public DateTime TimeStamp { get; protected set; }
        public string corelationId { get; set; }
        public string replyQueue { get; set; }
        public Message()

        {
            MessageType = GetType().Name;
            TimeStamp = DateTime.Now;

        }
    }
}
