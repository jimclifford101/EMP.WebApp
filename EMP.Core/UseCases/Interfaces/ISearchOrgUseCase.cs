using EMP.Core.DisplayViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMP.Core.UseCases.Interfaces
{

    public interface ISearchOrgUseCase
    {
        Task<IEnumerable<OrgSearchViewModel>> ExecuteAsync(string argsort = "", string argsortorder = "", OrgSearchInputModel argOrgSearchModel = null);
    }
}
