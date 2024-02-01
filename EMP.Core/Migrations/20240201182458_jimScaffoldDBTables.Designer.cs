﻿// <auto-generated />
using System;
using EMP.Core.DBRepos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EMP.Core.Migrations
{
    [DbContext(typeof(CoreDBContext))]
    [Migration("20240201182458_jimScaffoldDBTables")]
    partial class jimScaffoldDBTables
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("EMP.Core.Entities.Emp", b =>
                {
                    b.Property<int>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Guid"), 1L, 1);

                    b.Property<string>("Fname")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Lname")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<int?>("StId")
                        .HasColumnType("int");

                    b.HasKey("Guid")
                        .HasName("PK_cEmp");

                    b.HasIndex("StId");

                    b.ToTable("Emp");
                });

            modelBuilder.Entity("EMP.Core.Entities.EmpHist", b =>
                {
                    b.Property<int>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Guid"), 1L, 1);

                    b.Property<int>("CurrentEmp")
                        .HasColumnType("int");

                    b.Property<int>("GuidEmp")
                        .HasColumnType("int");

                    b.Property<int>("GuidOrg")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.HasKey("Guid")
                        .HasName("PK_cEmpHist");

                    b.HasIndex("GuidEmp");

                    b.HasIndex("GuidOrg");

                    b.ToTable("EmpHist");
                });

            modelBuilder.Entity("EMP.Core.Entities.Org", b =>
                {
                    b.Property<int>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Guid"), 1L, 1);

                    b.Property<string>("Orgname")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<int?>("StId")
                        .HasColumnType("int");

                    b.HasKey("Guid")
                        .HasName("PK_cOrg");

                    b.HasIndex("StId");

                    b.ToTable("Org");
                });

            modelBuilder.Entity("EMP.Core.Entities.PlState", b =>
                {
                    b.Property<int>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Guid"), 1L, 1);

                    b.Property<string>("StateName")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.HasKey("Guid")
                        .HasName("PK_cPlState");

                    b.ToTable("PlState");
                });

            modelBuilder.Entity("EMP.Core.Entities.Emp", b =>
                {
                    b.HasOne("EMP.Core.Entities.PlState", "St")
                        .WithMany("Emps")
                        .HasForeignKey("StId")
                        .HasConstraintName("FK_cEmp_stid");

                    b.Navigation("St");
                });

            modelBuilder.Entity("EMP.Core.Entities.EmpHist", b =>
                {
                    b.HasOne("EMP.Core.Entities.Emp", "GuidEmpNavigation")
                        .WithMany("EmpHists")
                        .HasForeignKey("GuidEmp")
                        .IsRequired()
                        .HasConstraintName("FK_cEmpHist_EmpId");

                    b.HasOne("EMP.Core.Entities.Org", "GuidOrgNavigation")
                        .WithMany("EmpHists")
                        .HasForeignKey("GuidOrg")
                        .IsRequired()
                        .HasConstraintName("FK_cEmpHist_OrgId");

                    b.Navigation("GuidEmpNavigation");

                    b.Navigation("GuidOrgNavigation");
                });

            modelBuilder.Entity("EMP.Core.Entities.Org", b =>
                {
                    b.HasOne("EMP.Core.Entities.PlState", "St")
                        .WithMany("Orgs")
                        .HasForeignKey("StId")
                        .HasConstraintName("FK_cOrg_stid");

                    b.Navigation("St");
                });

            modelBuilder.Entity("EMP.Core.Entities.Emp", b =>
                {
                    b.Navigation("EmpHists");
                });

            modelBuilder.Entity("EMP.Core.Entities.Org", b =>
                {
                    b.Navigation("EmpHists");
                });

            modelBuilder.Entity("EMP.Core.Entities.PlState", b =>
                {
                    b.Navigation("Emps");

                    b.Navigation("Orgs");
                });
#pragma warning restore 612, 618
        }
    }
}
