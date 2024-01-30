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
    public class ViewEmpHistByIdUseCase : IViewEmpHistByIdUseCase
    {
        private readonly IEmpHistRepository _EmpHistRepository;

        public ViewEmpHistByIdUseCase(IEmpHistRepository argEmpHistRepository)
        {
            this._EmpHistRepository = argEmpHistRepository;
        }

        public async Task<EmpHist> ExecuteAsync(int Guid)
        {
            return await this._EmpHistRepository.GetEmpHistByIdAsync(Guid);
        }

    }
}
