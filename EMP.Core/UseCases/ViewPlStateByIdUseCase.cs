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
    public class ViewPlStateByIdUseCase : IViewPlStateByIdUseCase
    {
        private readonly IPlStateRepository _PlStateRepository;

        public ViewPlStateByIdUseCase(IPlStateRepository argPlStateRepository)
        {
            this._PlStateRepository = argPlStateRepository;
        }

        public async Task<PlState> ExecuteAsync(int Guid)
        {
            return await this._PlStateRepository.GetPlStateByIdAsync(Guid);
        }

    }
}
