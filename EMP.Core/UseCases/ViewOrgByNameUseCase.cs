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
    public class ViewOrgByNameUseCase : IViewOrgByNameUseCase
    {
        private readonly IOrgRepository _OrgRepository;

        public ViewOrgByNameUseCase(IOrgRepository OrgRepository)
        {
            this._OrgRepository = OrgRepository;
        }

        public async Task<IEnumerable<Org>> ExecuteAsync(string name = "")
        {
            return await _OrgRepository.GetOrgByNameAsync(name);
        }
    }

}
