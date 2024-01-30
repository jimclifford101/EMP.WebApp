using EMP.Core.DisplayViewModels;
using EMP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMP.Core.DBRepos.Interfaces
{
    public interface IEmpRepository
    {
        Task<IEnumerable<Emp>> GetEmpAllAsync(string argsort = "", string argsortorder = "");
        Task<IEnumerable<Emp>> GetEmpByNameAsync(string name);
        Task<Emp> GetEmpByIdAsync(int argGuid);
        Task UpdateEmpAsync(Emp argEmp);
        Task AddEmpAsync(Emp argEmp);
        Task DeleteEmpAsync(Emp argEmp);
        Task<IEnumerable<EmpSearchViewModel>> SearchEmpAsync(string argsort = "", string argsortorder = "", EmpSearchModel argEmpSearchModel = null);

    }
}
