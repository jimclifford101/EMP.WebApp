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
    public class AddEmpHistUseCase : IAddEmpHistUseCase
    {
        private readonly IEmpHistRepository _EmpHistRepository;

        // Constructor. 
        public AddEmpHistUseCase(IEmpHistRepository argEmpHistRepository)
        {
            this._EmpHistRepository = argEmpHistRepository;
        }

        public async Task ExecuteAsync(EmpHist argEmpHist)
        {
            await this._EmpHistRepository.AddEmpHistAsync(argEmpHist);
        }
    }

}
