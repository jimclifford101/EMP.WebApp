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
    public class AddOrgUseCase : IAddOrgUseCase
    {
        private readonly IOrgRepository _OrgRepository;

        // Constructor. 
        public AddOrgUseCase(IOrgRepository argOrgRepository)
        {
            this._OrgRepository = argOrgRepository;
        }

        public async Task ExecuteAsync(Org argOrg)
        {
            await this._OrgRepository.AddOrgAsync(argOrg);
        }
    }
}
