using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Interfaces
{
    public abstract class DbModel
    {
        private readonly string Id;
        protected  DbModel()
        {
            Id = Guid.NewGuid().ToString();
            
        }
    }
}
