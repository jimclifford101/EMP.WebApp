using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EMP.Core.Entities
{
    [Table("Org")]
    public partial class Org
    {
        public Org()
        {
            EmpHists = new HashSet<EmpHist>();
        }

        [Key]
        public int Guid { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        public string? Orgname { get; set; }
        public int? StId { get; set; }

        [ForeignKey("StId")]
        [InverseProperty("Orgs")]
        public virtual PlState? St { get; set; }
        [InverseProperty("GuidOrgNavigation")]
        public virtual ICollection<EmpHist> EmpHists { get; set; }
    }
}
