using EMP.Core.DBRepos.Interfaces;
using EMP.Core.Entities;
using EMP.Core.UseCases.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMP.Core.UseCases
{
    public class ViewEmpAllUseCase : IViewEmpAllUseCase
    {
        private readonly IEmpRepository _EmpRepository;

        public ViewEmpAllUseCase(IEmpRepository EmpRepository)
        {
            this._EmpRepository = EmpRepository;
        }

        public async Task<IEnumerable<Emp>> ExecuteAsync(string argsort = "", string argsortorder = "")
        {
            return await _EmpRepository.GetEmpAllAsync(argsort, argsortorder);
        }
    }
}
