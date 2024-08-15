﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SustentApp.Infrastructure.Contexts;

#nullable disable

namespace SustentApp.Infrastructure.Migrations
{
    [DbContext(typeof(SustentAppContext))]
    [Migration("20240620000126_AddUserConfirmedEmail")]
    partial class AddUserConfirmedEmail
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("SustentApp.Domain.Users.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<bool>("ConfirmedEmail")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValue(false);

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("(now())");

                    b.Property<string>("Document")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("varchar(14)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("varchar(60)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("(now())");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SustentApp.Domain.Users.Entities.User", b =>
                {
                    b.OwnsOne("SustentApp.Domain.Users.Entities.Address", "Address", b1 =>
                        {
                            b1.Property<string>("UserId")
                                .HasColumnType("varchar(255)");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("varchar(100)");

                            b1.Property<string>("Complement")
                                .HasMaxLength(100)
                                .HasColumnType("varchar(100)");

                            b1.Property<string>("Neighborhood")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("varchar(100)");

                            b1.Property<string>("Number")
                                .IsRequired()
                                .HasMaxLength(10)
                                .HasColumnType("varchar(10)");

                            b1.Property<string>("State")
                                .IsRequired()
                                .HasMaxLength(2)
                                .HasColumnType("varchar(2)");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("varchar(100)");

                            b1.Property<string>("ZipCode")
                                .IsRequired()
                                .HasMaxLength(8)
                                .HasColumnType("varchar(8)");

                            b1.HasKey("UserId");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.Navigation("Address");
                });
#pragma warning restore 612, 618
        }
    }
}
