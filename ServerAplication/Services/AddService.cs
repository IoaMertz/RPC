using ServerAplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerAplication.Services
{
    public class AddService : ICalculation
    {
        public float CalculateAsync(int Number1 , int Number2 )
        {
            return Number1 + Number2;
        }
    }
}
