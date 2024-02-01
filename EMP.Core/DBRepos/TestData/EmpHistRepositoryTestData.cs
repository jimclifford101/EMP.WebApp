using EMP.Core.DBRepos.Interfaces;
using EMP.Core.DisplayViewModels;
using EMP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMP.Core.DBRepos.TestData
{
    public class EmpHistRepositoryTestData : IEmpHistRepository
    {
        private List<EmpHist> _EmpHistList;

        public EmpHistRepositoryTestData()
        {
            _EmpHistList = new List<EmpHist>()
            {
                new EmpHist { Guid = 1, Title = "ZZZ"},
                new EmpHist { Guid = 2, Title = "bbb"},
                new EmpHist { Guid = 3, Title = "MMM"},
                new EmpHist { Guid = 4, Title = "test"},
                new EmpHist { Guid = 5, Title = "ccc"},
                new EmpHist { Guid = 6, Title = "ddd"}
            };
        }

        public async Task<IEnumerable<EmpHistListViewModel>> GetEmpHistAllAsync(string argsort = "", string argsortorder = "")
        {
            return Enumerable.Empty<EmpHistListViewModel>();


        }

        public async Task<IEnumerable<EmpHist>> GetEmpHistByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return await Task.FromResult(_EmpHistList);

            return _EmpHistList.Where(x => x.Title.Contains(name, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<EmpHist> GetEmpHistByIdAsync(int argGuid)
        {
            var FoundItemByID = _EmpHistList.First(x => x.Guid == argGuid);
            var newItem = new EmpHist
            {
                Guid = FoundItemByID.Guid,
                Title = FoundItemByID.Title
            };

            return await Task.FromResult(newItem);
        }

        public Task AddEmpHistAsync(EmpHist argEmpHist)
        {
            if (_EmpHistList.Count > 0)
            {
                var maxId = _EmpHistList.Max(x => x.Guid);
                if (maxId != null && maxId > 0)
                {
                    argEmpHist.Guid = maxId + 1;
                }
                else
                {
                    argEmpHist.Guid = 1;
                }
            }
            else
            {
                argEmpHist.Guid = 1;
            }

            _EmpHistList.Add(argEmpHist);

            return Task.CompletedTask;
        }

        public Task UpdateEmpHistAsync(EmpHist argEmpHist)
        {
            var FoundItemByID = _EmpHistList.FirstOrDefault(x => x.Guid == argEmpHist.Guid);
            if (FoundItemByID != null)
            {
                FoundItemByID.Title = argEmpHist.Title;
            }

            return Task.CompletedTask;
        }

        public Task DeleteEmpHistAsync(EmpHist argEmpHist)
        {
            var FoundItemByID = _EmpHistList.FirstOrDefault(x => x.Guid == argEmpHist.Guid);
            if (FoundItemByID != null)
            {
                _EmpHistList.Remove(FoundItemByID);
            }

            return Task.CompletedTask;
        }
    }

}
