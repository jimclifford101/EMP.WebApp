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
    public class ViewEmpHistByNameUseCase : IViewEmpHistByNameUseCase
    {
        private readonly IEmpHistRepository _EmpHistRepository;

        public ViewEmpHistByNameUseCase(IEmpHistRepository EmpHistRepository)
        {
            this._EmpHistRepository = EmpHistRepository;
        }

        public async Task<IEnumerable<EmpHist>> ExecuteAsync(string name = "")
        {
            return await _EmpHistRepository.GetEmpHistByNameAsync(name);
        }
    }
}
