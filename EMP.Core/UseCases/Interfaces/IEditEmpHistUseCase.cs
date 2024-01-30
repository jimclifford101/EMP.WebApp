using EMP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMP.Core.UseCases.Interfaces
{

    public interface IEditEmpHistUseCase
    {
        Task ExecuteAsync(EmpHist argEmpHist);
    }
}
