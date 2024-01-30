using EMP.Core.UseCases.Interfaces;
using EMP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMP.Core.DBRepos.Interfaces;

namespace EMP.Core.UseCases
{
    public class ViewPlStateByNameUseCase : IViewPlStateByNameUseCase
    {
        private readonly IPlStateRepository _PlStateRepository;

        public ViewPlStateByNameUseCase(IPlStateRepository PlStateRepository)
        {
            this._PlStateRepository = PlStateRepository;
        }

        public async Task<IEnumerable<PlState>> ExecuteAsync(string name = "")
        {
            return await _PlStateRepository.GetPlStateByNameAsync(name);
        }
    }

}
