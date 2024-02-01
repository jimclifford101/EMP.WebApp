using EMP.Core.DBRepos.Interfaces;
using EMP.Core.DisplayViewModels;
using EMP.Core.Entities;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMP.Core.DBRepos.TestData
{
    public class EmpRepositoryTestData : IEmpRepository
    {
        private List<Emp> _EmpList;

        public EmpRepositoryTestData()
        {
            _EmpList = new List<Emp>()
            {
                new Emp { Guid = 1, Fname = "ZZZ"},
                new Emp { Guid = 2, Fname = "bbb"},
                new Emp { Guid = 3, Fname = "MMM"},
                new Emp { Guid = 4, Fname = "test"},
                new Emp { Guid = 5, Fname = "ccc"},
                new Emp { Guid = 6, Fname = "ddd"}
            };
        }

        //Task<IEnumerable<Emp>> SearchEmpAsync(string argsort = "", string argsortorder = "", string argWhere = "");
        public async Task<IEnumerable<EmpSearchViewModel>> SearchEmpAsync(string argsort = "", string argsortorder = "", EmpSearchModel argEmpSearchModel = null)
        {

            return Enumerable.Empty<EmpSearchViewModel>();


        }


        public async Task<IEnumerable<Emp>> GetEmpAllAsync(string argsort = "", string argsortorder = "")
        {
            if (argsort == "NAME")
            {
                if (argsortorder == "ASC")
                {
                    return await Task.FromResult(_EmpList.OrderBy(t => t.Fname));
                }
                else
                {
                    return await Task.FromResult(_EmpList.OrderByDescending(t => t.Fname));
                }
            }
            else
            {
                if (argsortorder == "ASC")
                {
                    return await Task.FromResult(_EmpList.OrderBy(t => t.Guid));
                }
                else
                {
                    return await Task.FromResult(_EmpList.OrderByDescending(t => t.Guid));
                }

            }
            //return await Task.FromResult(_EmpList);
        }

        public async Task<IEnumerable<Emp>> GetEmpByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return await Task.FromResult(_EmpList);

            return _EmpList.Where(x => x.Fname.Contains(name, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<Emp> GetEmpByIdAsync(int argGuid)
        {
            var FoundItemByID = _EmpList.First(x => x.Guid == argGuid);
            var newItem = new Emp
            {
                Guid = FoundItemByID.Guid,
                Fname = FoundItemByID.Fname
            };

            return await Task.FromResult(newItem);
        }

        public Task AddEmpAsync(Emp argEmp)
        {
            if (_EmpList.Count > 0)
            {
                var maxId = _EmpList.Max(x => x.Guid);
                if (maxId != null && maxId > 0)
                {
                    argEmp.Guid = maxId + 1;
                }
                else
                {
                    argEmp.Guid = 1;
                }
            }
            else
            {
                argEmp.Guid = 1;
            }

            _EmpList.Add(argEmp);

            return Task.CompletedTask;
        }

        public Task UpdateEmpAsync(Emp argEmp)
        {
            var FoundItemByID = _EmpList.FirstOrDefault(x => x.Guid == argEmp.Guid);
            if (FoundItemByID != null)
            {
                FoundItemByID.Fname = argEmp.Fname;
            }

            return Task.CompletedTask;
        }

        public Task DeleteEmpAsync(Emp argEmp)
        {
            var FoundItemByID = _EmpList.FirstOrDefault(x => x.Guid == argEmp.Guid);
            if (FoundItemByID != null)
            {
                _EmpList.Remove(FoundItemByID);
            }

            return Task.CompletedTask;
        }
    }

}
