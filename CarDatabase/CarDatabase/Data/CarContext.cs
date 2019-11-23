using CarDatabase.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace CarDatabase.Data
{
    public class CarContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbPath = Path.Combine(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                "mydb.db");
            optionsBuilder.UseSqlite($"Data Source={dbPath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ownership>()
                .HasIndex(o => new { o.CarModelID, o.OwnershipID })
                .IsUnique();

            modelBuilder.Entity<Ownership>()
                .HasIndex(o => o.VehicleIdentificationNumber)
                .IsUnique();

            modelBuilder.Entity<CarMake>()
                .HasIndex(cmk => cmk.Make)
                .IsUnique();

            modelBuilder.Entity<Person>()
                .HasMany(p => p.Ownerships)
                .WithOne(o => o.Person);

            modelBuilder.Entity<CarModel>()
                .HasMany(cm => cm.Ownerships)
                .WithOne(o => o.CarModel);

            modelBuilder.Entity<CarMake>()
                .HasMany(cmk => cmk.CarModels)
                .WithOne(cm => cm.CarMake);
        }

        public DbSet<Ownership> Ownerships { get; set; }

        public DbSet<Person> Persons { get; set; }

        public DbSet<CarModel> CarModels { get; set; }

        public DbSet<CarMake> CarMakes { get; set; }
    }
}
