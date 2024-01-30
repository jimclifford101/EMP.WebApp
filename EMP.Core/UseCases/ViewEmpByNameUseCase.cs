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
    public class ViewEmpByNameUseCase : IViewEmpByNameUseCase
    {
        private readonly IEmpRepository _EmpRepository;

        public ViewEmpByNameUseCase(IEmpRepository EmpRepository)
        {
            this._EmpRepository = EmpRepository;
        }

        public async Task<IEnumerable<Emp>> ExecuteAsync(string name = "")
        {
            return await _EmpRepository.GetEmpByNameAsync(name);
        }
    }
}
