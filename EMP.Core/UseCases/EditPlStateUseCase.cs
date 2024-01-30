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
    public class EditPlStateUseCase : IEditPlStateUseCase
    {
        private readonly IPlStateRepository _PlStateRepository;

        public EditPlStateUseCase(IPlStateRepository argPlStateRepository)
        {
            this._PlStateRepository = argPlStateRepository;
        }

        public async Task ExecuteAsync(PlState argPlState)
        {
            await this._PlStateRepository.UpdatePlStateAsync(argPlState);
        }

    }
}
