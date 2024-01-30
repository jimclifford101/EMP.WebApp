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
    public class DeleteEmpHistUseCase : IDeleteEmpHistUseCase
    {
        private readonly IEmpHistRepository _EmpHistRepository;

        public DeleteEmpHistUseCase(IEmpHistRepository argEmpHistRepository)
        {
            this._EmpHistRepository = argEmpHistRepository;
        }

        public async Task ExecuteAsync(EmpHist argEmpHist)
        {
            await this._EmpHistRepository.DeleteEmpHistAsync(argEmpHist);
        }

    }
}
