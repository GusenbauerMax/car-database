﻿// <auto-generated />
using CarDatabase.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CarDatabase.Migrations
{
    [DbContext(typeof(CarContext))]
    [Migration("20191121075719_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.1");

            modelBuilder.Entity("CarDatabase.Model.CarMake", b =>
                {
                    b.Property<int>("CarMakeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Make")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(100);

                    b.HasKey("CarMakeID");

                    b.ToTable("CarMakes");
                });

            modelBuilder.Entity("CarDatabase.Model.CarModel", b =>
                {
                    b.Property<int>("CarModelID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CarMakeID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(100);

                    b.HasKey("CarModelID");

                    b.HasIndex("CarMakeID");

                    b.ToTable("CarModels");
                });

            modelBuilder.Entity("CarDatabase.Model.Ownership", b =>
                {
                    b.Property<int>("OwnershipID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CarModelID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PersonID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("VehicleIdentificationNumber")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(100);

                    b.HasKey("OwnershipID");

                    b.HasIndex("PersonID");

                    b.HasIndex("VehicleIdentificationNumber")
                        .IsUnique();

                    b.HasIndex("CarModelID", "OwnershipID")
                        .IsUnique();

                    b.ToTable("Ownerships");
                });

            modelBuilder.Entity("CarDatabase.Model.Person", b =>
                {
                    b.Property<int>("PersonID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.HasKey("PersonID");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("CarDatabase.Model.CarModel", b =>
                {
                    b.HasOne("CarDatabase.Model.CarMake", "CarMake")
                        .WithMany("CarModels")
                        .HasForeignKey("CarMakeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CarDatabase.Model.Ownership", b =>
                {
                    b.HasOne("CarDatabase.Model.CarModel", "CarModel")
                        .WithMany("Ownerships")
                        .HasForeignKey("CarModelID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CarDatabase.Model.Person", "Person")
                        .WithMany("Ownerships")
                        .HasForeignKey("PersonID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
