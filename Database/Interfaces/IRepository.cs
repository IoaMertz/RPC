using Database.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Interfaces
{
    public interface IRepository<T> where T : DbModel
    {
         Task<CalculationDbModel> AddAsync(T dbModel);
         Task<IEnumerable<CalculationDbModel>> GetAllAsync();
    }
}
