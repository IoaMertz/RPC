using MessageBrokerDomain.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Messages
{
    public class CalculationResponseMessage :Message
    {
        public string ClientsID { get; set; }
        public string ClientsIP { get; set; }
        public int Number { get; set; }
        public string ServiceName { get; set; }
        public int Number1 { get; set; }
        public int Number2 { get; set; }
        public float Result { get; set; }

        public CalculationResponseMessage(string clientId, string clientsIP, int number1, int number2, string serviceName, float result)
        {
            ClientsID = clientId;
            ClientsIP = clientsIP;
            Number1 = number1;
            Number2 = number2;
            ServiceName = serviceName;
            Result = result;
        }
    }
}
