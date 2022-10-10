﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StuartAitken.Blazor.Server.DataAccess;

#nullable disable

namespace StuartAitken.Blazor.Server.Migrations
{
    [DbContext(typeof(StuartAitkenContext))]
    [Migration("20221009044711_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.9");

            modelBuilder.Entity("StuartAitken.Blazor.Server.DataAccess.Entities.PortfolioProject", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ProjectDate")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("ProjectDurationWeeks")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Tech")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Urls")
                        .HasColumnType("TEXT");

                    b.Property<int?>("Views")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.ToTable("PortfolioProject");
                });

            modelBuilder.Entity("StuartAitken.Blazor.Server.DataAccess.Entities.PortfolioProjectImage", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("ImageByteArray")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("TEXT");

                    b.Property<int?>("PrimaryImage")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProjectId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("ProjectId");

                    b.ToTable("PortfolioProjectImage");
                });

            modelBuilder.Entity("StuartAitken.Blazor.Server.DataAccess.Entities.PortfolioProjectTech", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("PortfolioProjectTech");
                });

            modelBuilder.Entity("StuartAitken.Blazor.Server.DataAccess.Entities.PortfolioProjectType", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("PortfolioProjectType");
                });

            modelBuilder.Entity("StuartAitken.Blazor.Server.DataAccess.Entities.PortfolioProjectImage", b =>
                {
                    b.HasOne("StuartAitken.Blazor.Server.DataAccess.Entities.PortfolioProject", "Project")
                        .WithMany("Images")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");
                });

            modelBuilder.Entity("StuartAitken.Blazor.Server.DataAccess.Entities.PortfolioProject", b =>
                {
                    b.Navigation("Images");
                });
#pragma warning restore 612, 618
        }
    }
}
