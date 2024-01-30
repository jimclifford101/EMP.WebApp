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
    public class ViewEmpByIdUseCase : IViewEmpByIdUseCase
    {
        private readonly IEmpRepository _EmpRepository;

        public ViewEmpByIdUseCase(IEmpRepository argEmpRepository)
        {
            this._EmpRepository = argEmpRepository;
        }

        public async Task<Emp> ExecuteAsync(int Guid)
        {
            return await this._EmpRepository.GetEmpByIdAsync(Guid);
        }

    }
}
