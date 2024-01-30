using EMP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMP.Core.DBRepos.Interfaces
{
    public interface IPlStateRepository
    {
        Task<IEnumerable<PlState>> GetPlStateAllAsync(string argsort = "", string argsortorder = "");
        Task<IEnumerable<PlState>> GetPlStateByNameAsync(string name);
        Task<PlState> GetPlStateByIdAsync(int argGuid);
        Task UpdatePlStateAsync(PlState argPlState);
        Task AddPlStateAsync(PlState argPlState);
        Task DeletePlStateAsync(PlState argPlState);
    }
}
