using Database.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.DbModels
{
    public class CalculationDbModel  :DbModel
    {
        public int ClientsID { get; set; }
        public int ClientsIP { get; set; }
        public int Number1 { get; set; }
        public int Number2 { get; set; }
        public float Result { get; set; }
        public string ServiceName { get; set; }
    }
}
