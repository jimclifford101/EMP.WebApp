﻿using EMP.Core.DBRepos.Interfaces;
using EMP.Core.Entities;
using EMP.Core.UseCases.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMP.Core.UseCases
{
    public class EditEmpUseCase : IEditEmpUseCase
    {
        private readonly IEmpRepository _EmpRepository;

        public EditEmpUseCase(IEmpRepository argEmpRepository)
        {
            this._EmpRepository = argEmpRepository;
        }

        public async Task ExecuteAsync(Emp argEmp)
        {
            await this._EmpRepository.UpdateEmpAsync(argEmp);
        }

    }
}
