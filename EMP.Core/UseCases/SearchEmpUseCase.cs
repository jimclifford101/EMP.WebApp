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


    public class SearchEmpUseCase : ISearchEmpUseCase
    {
        private readonly IEmpRepository _EmpRepository;

        public SearchEmpUseCase(IEmpRepository EmpRepository)
        {
            this._EmpRepository = EmpRepository;
        }

        public async Task<IEnumerable<EmpSearchViewModel>> ExecuteAsync(string argsort = "", string argsortorder = "", EmpSearchModel argEmpSearchModel = null)
        {
            return await _EmpRepository.SearchEmpAsync(argsort, argsortorder, argEmpSearchModel);
        }
    }
}
