﻿// <auto-generated />
using System;
using APISoP.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace APISoP.Data.Migrations
{
    [DbContext(typeof(ApiSoPDbContext))]
    [Migration("20210608224208_create_principal_tables_for_registers")]
    partial class create_principal_tables_for_registers
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("APISoP.CrossCutting.Entities.CashRegister", b =>
                {
                    b.Property<Guid>("CashRegisterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("StoreId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CashRegisterId");

                    b.HasIndex("StoreId");

                    b.ToTable("CashRegisters");
                });

            modelBuilder.Entity("APISoP.CrossCutting.Entities.Enterprise", b =>
                {
                    b.Property<Guid>("EnterpriseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<Guid>("MembershipId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Phone")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("datetime2");

                    b.HasKey("EnterpriseId");

                    b.HasIndex("MembershipId")
                        .IsUnique();

                    b.ToTable("Enterprises");
                });

            modelBuilder.Entity("APISoP.CrossCutting.Entities.Membership", b =>
                {
                    b.Property<Guid>("MembershipId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int>("TypeMembership")
                        .HasColumnType("int");

                    b.HasKey("MembershipId");

                    b.ToTable("Memberships");
                });

            modelBuilder.Entity("APISoP.CrossCutting.Entities.Permission", b =>
                {
                    b.Property<Guid>("PermissionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Action")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Module")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("datetime2");

                    b.HasKey("PermissionId");

                    b.ToTable("Permissions");
                });

            modelBuilder.Entity("APISoP.CrossCutting.Entities.Profile", b =>
                {
                    b.Property<Guid>("ProfileId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("StoreId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ProfileId");

                    b.HasIndex("StoreId");

                    b.ToTable("Profiles");
                });

            modelBuilder.Entity("APISoP.CrossCutting.Entities.ProfilePermissions", b =>
                {
                    b.Property<Guid>("ProfilePermissionsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Enable")
                        .HasColumnType("bit");

                    b.Property<Guid>("PermissionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProfileId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ProfilePermissionsId");

                    b.HasIndex("PermissionId");

                    b.HasIndex("ProfileId");

                    b.ToTable("ProfilePermissions");
                });

            modelBuilder.Entity("APISoP.CrossCutting.Entities.Store", b =>
                {
                    b.Property<Guid>("StoreId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("EnterpriseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StoreDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StoreName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StoreId");

                    b.HasIndex("EnterpriseId");

                    b.ToTable("Stores");
                });

            modelBuilder.Entity("APISoP.CrossCutting.Entities.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CellPhone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ProfileId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("StoreId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("TypeUser")
                        .HasColumnType("int");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.HasIndex("ProfileId");

                    b.HasIndex("StoreId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("APISoP.CrossCutting.Entities.CashRegister", b =>
                {
                    b.HasOne("APISoP.CrossCutting.Entities.Store", "Store")
                        .WithMany("CashRegisters")
                        .HasForeignKey("StoreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Store");
                });

            modelBuilder.Entity("APISoP.CrossCutting.Entities.Enterprise", b =>
                {
                    b.HasOne("APISoP.CrossCutting.Entities.Membership", "Membership")
                        .WithOne("Enterprise")
                        .HasForeignKey("APISoP.CrossCutting.Entities.Enterprise", "MembershipId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Membership");
                });

            modelBuilder.Entity("APISoP.CrossCutting.Entities.Profile", b =>
                {
                    b.HasOne("APISoP.CrossCutting.Entities.Store", "Store")
                        .WithMany("Profiles")
                        .HasForeignKey("StoreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Store");
                });

            modelBuilder.Entity("APISoP.CrossCutting.Entities.ProfilePermissions", b =>
                {
                    b.HasOne("APISoP.CrossCutting.Entities.Permission", "Permission")
                        .WithMany()
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("APISoP.CrossCutting.Entities.Profile", "Profile")
                        .WithMany("Permissions")
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Permission");

                    b.Navigation("Profile");
                });

            modelBuilder.Entity("APISoP.CrossCutting.Entities.Store", b =>
                {
                    b.HasOne("APISoP.CrossCutting.Entities.Enterprise", "Enterprise")
                        .WithMany("Stores")
                        .HasForeignKey("EnterpriseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Enterprise");
                });

            modelBuilder.Entity("APISoP.CrossCutting.Entities.User", b =>
                {
                    b.HasOne("APISoP.CrossCutting.Entities.Profile", "Profile")
                        .WithMany()
                        .HasForeignKey("ProfileId");

                    b.HasOne("APISoP.CrossCutting.Entities.Store", "Store")
                        .WithMany("Users")
                        .HasForeignKey("StoreId");

                    b.Navigation("Profile");

                    b.Navigation("Store");
                });

            modelBuilder.Entity("APISoP.CrossCutting.Entities.Enterprise", b =>
                {
                    b.Navigation("Stores");
                });

            modelBuilder.Entity("APISoP.CrossCutting.Entities.Membership", b =>
                {
                    b.Navigation("Enterprise");
                });

            modelBuilder.Entity("APISoP.CrossCutting.Entities.Profile", b =>
                {
                    b.Navigation("Permissions");
                });

            modelBuilder.Entity("APISoP.CrossCutting.Entities.Store", b =>
                {
                    b.Navigation("CashRegisters");

                    b.Navigation("Profiles");

                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}