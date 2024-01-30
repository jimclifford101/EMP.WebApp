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
    public class ViewOrgByIdUseCase : IViewOrgByIdUseCase
    {
        private readonly IOrgRepository _OrgRepository;

        public ViewOrgByIdUseCase(IOrgRepository argOrgRepository)
        {
            this._OrgRepository = argOrgRepository;
        }

        public async Task<Org> ExecuteAsync(int Guid)
        {
            return await this._OrgRepository.GetOrgByIdAsync(Guid);
        }

    }
}
