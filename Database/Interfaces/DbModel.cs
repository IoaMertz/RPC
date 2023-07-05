using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Interfaces
{
    public abstract class DbModel
    {
        [JsonProperty("id")]
        public readonly string Id;
        protected  DbModel()
        {
            Id = Guid.NewGuid().ToString();
            
        }
    }
}
