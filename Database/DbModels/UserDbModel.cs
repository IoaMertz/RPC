using Database.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.DbModels
{
    public class UserDbModel :DbModel
    {
        [JsonProperty("UserName")]
        public string UserName { get; set; }

        [JsonProperty("UserPassword")]
        public string Password { get; set; }
    }
}
