using EMP.Core.DBRepos.Interfaces;
using EMP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMP.Core.DBRepos
{
    public class OrgRepositoryTestData : IOrgRepository
    {
        private List<Org> _OrgList;

        public OrgRepositoryTestData()
        {
            _OrgList = new List<Org>()
            {
                new Org { Guid = 1, Orgname = "ZZZ"},
                new Org { Guid = 2, Orgname = "bbb"},
                new Org { Guid = 3, Orgname = "MMM"},
                new Org { Guid = 4, Orgname = "test"},
                new Org { Guid = 5, Orgname = "ccc"},
                new Org { Guid = 6, Orgname = "ddd"}
            };
        }

        public async Task<IEnumerable<Org>> GetOrgAllAsync(string argsort = "", string argsortorder = "")
        {
            if (argsort == "NAME")
            {
                if (argsortorder == "ASC")
                {
                    return await Task.FromResult(_OrgList.OrderBy(t => t.Orgname));
                }
                else
                {
                    return await Task.FromResult(_OrgList.OrderByDescending(t => t.Orgname));
                }
            }
            else
            {
                if (argsortorder == "ASC")
                {
                    return await Task.FromResult(_OrgList.OrderBy(t => t.Guid));
                }
                else
                {
                    return await Task.FromResult(_OrgList.OrderByDescending(t => t.Guid));
                }

            }
            //return await Task.FromResult(_OrgList);
        }

        public async Task<IEnumerable<Org>> GetOrgByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return await Task.FromResult(_OrgList);

            return _OrgList.Where(x => x.Orgname.Contains(name, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<Org> GetOrgByIdAsync(int argGuid)
        {
            var FoundItemByID = _OrgList.First(x => x.Guid == argGuid);
            var newItem = new Org
            {
                Guid = FoundItemByID.Guid,
                Orgname = FoundItemByID.Orgname
            };

            return await Task.FromResult(newItem);
        }

        public Task AddOrgAsync(Org argOrg)
        {
            if (_OrgList.Count > 0)
            {
                var maxId = _OrgList.Max(x => x.Guid);
                if (maxId != null && maxId > 0)
                {
                    argOrg.Guid = maxId + 1;
                }
                else
                {
                    argOrg.Guid = 1;
                }
            }
            else
            {
                argOrg.Guid = 1;
            }

            _OrgList.Add(argOrg);

            return Task.CompletedTask;
        }

        public Task UpdateOrgAsync(Org argOrg)
        {
            var FoundItemByID = _OrgList.FirstOrDefault(x => x.Guid == argOrg.Guid);
            if (FoundItemByID != null)
            {
                FoundItemByID.Orgname = argOrg.Orgname;
            }

            return Task.CompletedTask;
        }

        public Task DeleteOrgAsync(Org argOrg)
        {
            var FoundItemByID = _OrgList.FirstOrDefault(x => x.Guid == argOrg.Guid);
            if (FoundItemByID != null)
            {
                _OrgList.Remove(FoundItemByID);
            }

            return Task.CompletedTask;
        }
    }
}
