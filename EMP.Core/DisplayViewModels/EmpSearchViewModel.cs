using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMP.Core.DisplayViewModels
{
    public class EmpSearchViewModel
    {

        public int EmpGuid { get; set; }
        public string Fname { get; set; } = string.Empty;
        public string Lname { get; set; } = string.Empty;
        public string StateName { get; set; } = string.Empty;

    }
}
