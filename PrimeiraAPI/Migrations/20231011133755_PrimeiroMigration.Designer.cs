﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PrimeiraAPI.Infrastructure;

#nullable disable

namespace PrimeiraAPI.Migrations
{
    [DbContext(typeof(ConnectionContext))]
    [Migration("20231011133755_PrimeiroMigration")]
    partial class PrimeiroMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PrimeiraAPI.Domain.Model.CompanyAggregate.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Nome")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("company");
                });

            modelBuilder.Entity("PrimeiraAPI.Domain.Model.EmployeeAggregate.Employee", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("id"));

                    b.Property<int>("age")
                        .HasColumnType("int");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("photo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("employee");
                });
#pragma warning restore 612, 618
        }
    }
}
