using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class CalculationRequestObject
    {
        public string ClientsID { get; set; }
        public string ClientsIP { get; set; }
        public int Number { get; set; }
        public string ServiceName { get; set; }
        public int Number1 { get; set; }
        public int Number2 { get; set; }
    }
}
