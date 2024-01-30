using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EMP.Core.Entities
{
    [Table("Emp")]
    public partial class Emp
    {
        public Emp()
        {
            EmpHists = new HashSet<EmpHist>();
        }

        [Key]
        public int Guid { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        public string? Fname { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        public string? Lname { get; set; }
        public int? StId { get; set; }

        [ForeignKey("StId")]
        [InverseProperty("Emps")]
        public virtual PlState? St { get; set; }
        [InverseProperty("GuidEmpNavigation")]
        public virtual ICollection<EmpHist> EmpHists { get; set; }
    }
}
