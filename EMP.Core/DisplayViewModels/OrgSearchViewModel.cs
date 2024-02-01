using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMP.Core.DisplayViewModels
{
    public class OrgSearchViewModel
    {
        // All vales Displayed from Search. All values in SELECT part of SQL Statement

        public int Guid { get; set; }
        public string Orgname { get; set; } = string.Empty;

    }
}
