using EMP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMP.Core.DBRepos.Interfaces
{
    public interface IOrgRepository
    {
        Task<IEnumerable<Org>> GetOrgAllAsync(string argsort = "", string argsortorder = "");
        Task<IEnumerable<Org>> GetOrgByNameAsync(string name);
        Task<Org> GetOrgByIdAsync(int argGuid);
        Task UpdateOrgAsync(Org argOrg);
        Task AddOrgAsync(Org argOrg);
        Task DeleteOrgAsync(Org argOrg);
    }
}
