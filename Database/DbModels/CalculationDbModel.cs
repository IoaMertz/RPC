using Database.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.DbModels
{
    public class CalculationDbModel  :DbModel
    {
        [JsonProperty("Clientsid")]
        public string ClientsID { get; set; }
        [JsonProperty("ClientsIP")]
        public string ClientsIP { get; set; }
        [JsonProperty("Number1")]
        public int Number1 { get; set; }
        [JsonProperty("Number2")]
        public int Number2 { get; set; }
        [JsonProperty("Result")]
        public float Result { get; set; }
        [JsonProperty("ServiceName")]
        public string ServiceName { get; set; }
        public CalculationDbModel()
        {
            
        }

        public CalculationDbModel(string clientsID, string clientsIP, int number1, int number2, float result, string serviceName)
        {
            ClientsID = clientsID;
            ClientsIP = clientsIP;
            Number1 = number1;
            Number2 = number2;
            Result = result;
            ServiceName = serviceName;
        }
    }
}
