using EMP.Core.DBRepos.Interfaces;
using EMP.Core.DisplayViewModels;
using EMP.Core.UseCases.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMP.Core.UseCases
{

    public class SearchOrgUseCase : ISearchOrgUseCase
    {
        private readonly IOrgRepository _OrgRepository;

        public SearchOrgUseCase(IOrgRepository OrgRepository)
        {
            this._OrgRepository = OrgRepository;
        }

        public async Task<IEnumerable<OrgSearchViewModel>> ExecuteAsync(string argsort = "", string argsortorder = "", OrgSearchInputModel argOrgSearchModel = null)
        {
            return await _OrgRepository.SearchOrgAsync(argsort, argsortorder, argOrgSearchModel);
        }
    }
}
