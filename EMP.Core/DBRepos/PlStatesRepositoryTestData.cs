using EMP.Core.DBRepos.Interfaces;
using EMP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMP.Core.DBRepos
{
    public class PlStateRepositoryTestData : IPlStateRepository
    {
        private List<PlState> _PlStateList;

        public PlStateRepositoryTestData()
        {
            _PlStateList = new List<PlState>()
            {
                new PlState { Guid = 1, StateName = "CA"},
                new PlState { Guid = 2, StateName = "CT"},
                new PlState { Guid = 3, StateName = "MA"},
                new PlState { Guid = 4, StateName = "MI"},
                new PlState { Guid = 5, StateName = "TN"},

            };
        }

        public async Task<IEnumerable<PlState>> GetPlStateAllAsync(string argsort = "", string argsortorder = "")
        {
            if (argsort == "NAME")
            {
                if (argsortorder == "ASC")
                {
                    return await Task.FromResult(_PlStateList.OrderBy(t => t.StateName));
                }
                else
                {
                    return await Task.FromResult(_PlStateList.OrderByDescending(t => t.StateName));
                }
            }
            else
            {
                if (argsortorder == "ASC")
                {
                    return await Task.FromResult(_PlStateList.OrderBy(t => t.Guid));
                }
                else
                {
                    return await Task.FromResult(_PlStateList.OrderByDescending(t => t.Guid));
                }

            }
            //return await Task.FromResult(_PlStateList);
        }

        public async Task<IEnumerable<PlState>> GetPlStateByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return await Task.FromResult(_PlStateList);

            return _PlStateList.Where(x => x.StateName.Contains(name, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<PlState> GetPlStateByIdAsync(int argGuid)
        {
            var FoundItemByID = _PlStateList.First(x => x.Guid == argGuid);
            var newItem = new PlState
            {
                Guid = FoundItemByID.Guid,
                StateName = FoundItemByID.StateName
            };

            return await Task.FromResult(newItem);
        }

        public Task AddPlStateAsync(PlState argPlState)
        {
            if (_PlStateList.Count > 0)
            {
                var maxId = _PlStateList.Max(x => x.Guid);
                if (maxId != null && maxId > 0)
                {
                    argPlState.Guid = maxId + 1;
                }
                else
                {
                    argPlState.Guid = 1;
                }
            }
            else
            {
                argPlState.Guid = 1;
            }

            _PlStateList.Add(argPlState);

            return Task.CompletedTask;
        }

        public Task UpdatePlStateAsync(PlState argPlState)
        {
            var FoundItemByID = _PlStateList.FirstOrDefault(x => x.Guid == argPlState.Guid);
            if (FoundItemByID != null)
            {
                FoundItemByID.StateName = argPlState.StateName;
            }

            return Task.CompletedTask;
        }

        public Task DeletePlStateAsync(PlState argPlState)
        {
            var FoundItemByID = _PlStateList.FirstOrDefault(x => x.Guid == argPlState.Guid);
            if (FoundItemByID != null)
            {
                _PlStateList.Remove(FoundItemByID);
            }

            return Task.CompletedTask;
        }
    }

}
