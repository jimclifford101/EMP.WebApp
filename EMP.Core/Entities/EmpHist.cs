using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EMP.Core.Entities
{
    [Table("EmpHist")]
    public partial class EmpHist
    {
        [Key]
        public int Guid { get; set; }
        public int GuidEmp { get; set; }
        public int GuidOrg { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        public string? Title { get; set; }
        public int CurrentEmp { get; set; }

        [ForeignKey("GuidEmp")]
        [InverseProperty("EmpHists")]
        public virtual Emp GuidEmpNavigation { get; set; } = null!;
        [ForeignKey("GuidOrg")]
        [InverseProperty("EmpHists")]
        public virtual Org GuidOrgNavigation { get; set; } = null!;
    }
}
