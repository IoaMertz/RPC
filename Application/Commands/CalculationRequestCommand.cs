using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace Application.Commands
{
    public class CalculationRequestCommand : IRequest<bool>
    {
        public string ClientsID { get; set; }
        public string ClientsIP { get; set; }
        public int Number { get; set; }
        public string ServiceName { get; set; }
        public int Number1 { get; set; }
        public int Number2 { get; set; }
        public float Result { get; set; }

        public CalculationRequestCommand(string clientID,string clientIP,int number1,int number2, string serviceName )
        {
            ClientsID = clientID;
            ClientsIP = clientIP;
            Number1 = number1;
            Number2 = number2;
            ServiceName = serviceName;
        }

    }
}
