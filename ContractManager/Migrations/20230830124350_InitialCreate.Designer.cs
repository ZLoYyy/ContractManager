﻿// <auto-generated />
using System;
using ContractManager.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ContractManager.Migrations
{
    [DbContext(typeof(ContractManagerDBContext))]
    [Migration("20230830124350_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ContractManager.Models.Contract", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Caption")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Client")
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Contracts");
                });

            modelBuilder.Entity("ContractManager.Models.ContractStage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Caption")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ContractId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateBegin")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateEnd")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ContractId");

                    b.ToTable("StageCantracts");
                });

            modelBuilder.Entity("ContractManager.Models.ContractStage", b =>
                {
                    b.HasOne("ContractManager.Models.Contract", null)
                        .WithMany("StageCantracts")
                        .HasForeignKey("ContractId");
                });

            modelBuilder.Entity("ContractManager.Models.Contract", b =>
                {
                    b.Navigation("StageCantracts");
                });
#pragma warning restore 612, 618
        }
    }
}
