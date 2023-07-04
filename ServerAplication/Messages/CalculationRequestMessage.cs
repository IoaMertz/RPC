using MessageBrokerDomain.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerAplication.Messages
{
    public class CalculationRequestMessage : Message
    {
        public int ClientsID { get; set; }  
        public int ClientsIP { get; set; }
        public int Number1 { get; set; }
        public int Number2 { get; set; }
        public float Result { get; set; }
        public string ServiceName { get; set; }
        public CalculationRequestMessage(int number1,int number2,float Result, string serviceName)
        {
            Number1 = number1;
            Number2 = number2;
            ServiceName = serviceName;
        }
    }
}
