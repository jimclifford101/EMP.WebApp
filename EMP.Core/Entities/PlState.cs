using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EMP.Core.Entities
{
    [Table("PlState")]
    public partial class PlState
    {
        public PlState()
        {
            Emps = new HashSet<Emp>();
            Orgs = new HashSet<Org>();
        }

        [Key]
        public int Guid { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        public string? StateName { get; set; }
        [StringLength(255)]

        [InverseProperty("St")]
        public virtual ICollection<Emp> Emps { get; set; }
        [InverseProperty("St")]
        public virtual ICollection<Org> Orgs { get; set; }
    }
}
