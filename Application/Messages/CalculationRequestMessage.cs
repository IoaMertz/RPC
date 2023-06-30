using MessageBrokerDomain.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Messages
{
    public class CalculationRequestMessage : Message
    {
        public int Number { get; set; }
        public string ServiceName { get; set; }

        public CalculationRequestMessage(int number ,string serviceName)
        {
            Number = number;
            ServiceName = serviceName;
        }
    }
}
