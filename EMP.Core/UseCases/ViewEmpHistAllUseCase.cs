using EMP.Core.DBRepos.Interfaces;
using EMP.Core.DisplayViewModels;
using EMP.Core.Entities;
using EMP.Core.UseCases.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMP.Core.UseCases
{
    public class ViewEmpHistAllUseCase : IViewEmpHistAllUseCase
    {
        private readonly IEmpHistRepository _EmpHistRepository;

        public ViewEmpHistAllUseCase(IEmpHistRepository EmpHistRepository)
        {
            this._EmpHistRepository = EmpHistRepository;
        }

        public async Task<IEnumerable<EmpHistListViewModel>> ExecuteAsync(string argsort = "", string argsortorder = "")
        {
            return await _EmpHistRepository.GetEmpHistAllAsync(argsort, argsortorder);
        }
    }
}
