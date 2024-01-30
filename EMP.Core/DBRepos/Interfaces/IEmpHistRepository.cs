using EMP.Core.DisplayViewModels;
using EMP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMP.Core.DBRepos.Interfaces
{
    public interface IEmpHistRepository
    {
        Task<IEnumerable<EmpHistListViewModel>> GetEmpHistAllAsync(string argsort = "", string argsortorder = "");
        Task<IEnumerable<EmpHist>> GetEmpHistByNameAsync(string name);
        Task<EmpHist> GetEmpHistByIdAsync(int argGuid);
        Task UpdateEmpHistAsync(EmpHist argEmpHist);
        Task AddEmpHistAsync(EmpHist argEmpHist);
        Task DeleteEmpHistAsync(EmpHist argEmpHist);
    }
}
