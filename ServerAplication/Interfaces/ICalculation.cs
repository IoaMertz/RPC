using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerAplication.Interfaces
{
    public interface ICalculation
    {
        float CalculateAsync(int Number1 , int Number2);
    }
}
