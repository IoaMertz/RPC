using ServerAplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerAplication.Services
{
    public class MultiplyService : ICalculation
    {
        public Task<int> CalculateAsync(int Number)
        {
            return Task.FromResult(Number * 2);
        }
    }
}
