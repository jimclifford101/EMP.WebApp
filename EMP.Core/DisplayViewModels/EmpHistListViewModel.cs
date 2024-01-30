using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMP.Core.DisplayViewModels
{


    public class EmpHistListViewModel
    {

        public int EmpHistGuid { get; set; }
        public int EmpId { get; set; }
        public string Fname { get; set; } = string.Empty;
        public string Lname { get; set; } = string.Empty;
        public int OrgId { get; set; }
        public string Orgname { get; set; } = string.Empty;
        
        public string Title { get; set; } = string.Empty;
        public string CurrentEmp { get; set; } = string.Empty;

    }

}
