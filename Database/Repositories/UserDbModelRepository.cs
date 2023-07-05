using Database.DbModels;
using Database.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Repositories
{
    public class UserDbModelRepository : IRepository<UserDbModel>
    {
        public Task<CalculationDbModel> AddAsync(UserDbModel dbModel)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CalculationDbModel>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
