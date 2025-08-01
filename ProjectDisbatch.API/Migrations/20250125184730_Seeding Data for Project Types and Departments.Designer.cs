﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ProjectDisbatch.API.Data;

#nullable disable

namespace ProjectDisbatch.API.Migrations
{
    [DbContext(typeof(ProjectDisbatchDbContext))]
    [Migration("20250125184730_Seeding Data for Project Types and Departments")]
    partial class SeedingDataforProjectTypesandDepartments
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ProjectDisbatch.API.Models.Domain.Department", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Departments");

                    b.HasData(
                        new
                        {
                            Id = new Guid("bb7376ed-d846-4a13-962f-0e5f6e7370c5"),
                            Code = "1A",
                            Description = "Responsible for taking on new inventions",
                            Name = "New Inventions"
                        },
                        new
                        {
                            Id = new Guid("994e60b1-778b-4f76-b9f0-ee15c91dd63e"),
                            Code = "1B",
                            Description = "Responsible for experimental design",
                            Name = "Experimental"
                        },
                        new
                        {
                            Id = new Guid("fe4846ac-0060-4cb1-a2ba-74270ad8055c"),
                            Code = "1C",
                            Description = "Responsible for research",
                            Name = "Research"
                        });
                });

            modelBuilder.Entity("ProjectDisbatch.API.Models.Domain.Project", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("DepartmentId")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("ProjectTypeId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("ProjectTypeId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("ProjectDisbatch.API.Models.Domain.ProjectType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ProjectTypes");

                    b.HasData(
                        new
                        {
                            Id = new Guid("c1b2b207-d7cc-4fff-a637-0fcac3aa7dce"),
                            Name = "Engineering"
                        },
                        new
                        {
                            Id = new Guid("b0770281-e108-4329-83a4-513cd3146eca"),
                            Name = "Geoscience"
                        },
                        new
                        {
                            Id = new Guid("fb07258c-7bd6-4905-8b66-a5fef02fae07"),
                            Name = "Rocketry"
                        });
                });

            modelBuilder.Entity("ProjectDisbatch.API.Models.Domain.Project", b =>
                {
                    b.HasOne("ProjectDisbatch.API.Models.Domain.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectDisbatch.API.Models.Domain.ProjectType", "ProjectType")
                        .WithMany()
                        .HasForeignKey("ProjectTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");

                    b.Navigation("ProjectType");
                });
#pragma warning restore 612, 618
        }
    }
}
