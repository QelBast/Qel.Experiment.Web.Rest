﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Qel.Ef.Contexts.BlacklistContext;

#nullable disable

namespace Qel.Ef.DesignTimeUtils.Migrations.Blacklist
{
    [DbContext(typeof(DbContextBlacklist))]
    [Migration("20241113072954_Seed")]
    partial class Seed
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Qel.Ef.Models.BlacklistContext.BlacklistedPassportData", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Serie")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("BlacklistedPassportData");
                });

            modelBuilder.Entity("Qel.Ef.Models.BlacklistContext.BlacklistedPerson", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("Birthdate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<long>("PassportId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("PassportId");

                    b.ToTable("BlacklistedPeson");
                });

            modelBuilder.Entity("Qel.Ef.Models.BlacklistContext.BlacklistedPerson", b =>
                {
                    b.HasOne("Qel.Ef.Models.BlacklistContext.BlacklistedPassportData", "Passport")
                        .WithMany()
                        .HasForeignKey("PassportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Passport");
                });
#pragma warning restore 612, 618
        }
    }
}
