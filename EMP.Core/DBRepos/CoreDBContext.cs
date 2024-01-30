


using EMP.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EMP.Core.DBRepos
{
    public class CoreDBContext : DbContext
    {
        public CoreDBContext(DbContextOptions<CoreDBContext> options) : base(options)
        {
        }

        public virtual DbSet<Emp> Emp { get; set; } = null!;
        public virtual DbSet<EmpHist> EmpHist { get; set; } = null!;
        public virtual DbSet<Org> Org { get; set; } = null!;
        public virtual DbSet<PlState> PlState { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Emp>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK_cEmp");

                entity.HasOne(d => d.St)
                    .WithMany(p => p.Emps)
                    .HasForeignKey(d => d.StId)
                    .HasConstraintName("FK_cEmp_stid");
            });

            modelBuilder.Entity<EmpHist>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK_cEmpHist");

                entity.HasOne(d => d.GuidEmpNavigation)
                    .WithMany(p => p.EmpHists)
                    .HasForeignKey(d => d.GuidEmp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_cEmpHist_EmpId");

                entity.HasOne(d => d.GuidOrgNavigation)
                    .WithMany(p => p.EmpHists)
                    .HasForeignKey(d => d.GuidOrg)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_cEmpHist_OrgId");
            });

            modelBuilder.Entity<Org>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK_cOrg");

                entity.HasOne(d => d.St)
                    .WithMany(p => p.Orgs)
                    .HasForeignKey(d => d.StId)
                    .HasConstraintName("FK_cOrg_stid");
            });

            modelBuilder.Entity<PlState>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK_cPlState");
            });

        }
    }
}




