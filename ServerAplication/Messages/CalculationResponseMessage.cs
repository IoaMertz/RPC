using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerAplication.Messages
{
    public class CalculationResponseMessage
    {
        public int Number1 { get; set; }
        public int Number2 { get; set; }
        public float Result { get; set; }
        public string ServiceName { get; set; }
        public CalculationResponseMessage(int number1, int number2, float result, string serviceName)
        {
            Number1 = number1;
            Number2 = number2;
            Result = result;
            ServiceName = serviceName;
        }
    }
}
