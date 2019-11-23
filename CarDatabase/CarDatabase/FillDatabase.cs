using CarDatabase.Data;
using CarDatabase.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CarDatabase
{
    partial class Program
    {
        static async Task CleanDatabaseAsync(CarContext context)
        {
            using var transaction = await context.Database.BeginTransactionAsync();

            await context.Database.ExecuteSqlRawAsync("DELETE FROM Persons");
            await context.Database.ExecuteSqlRawAsync("DELETE FROM CarModels");
            await context.Database.ExecuteSqlRawAsync("DELETE FROM CarMakes");
            await context.Database.ExecuteSqlRawAsync("DELETE FROM Ownerships");

            await transaction.CommitAsync();

            // DELETE FROM BOOKS
        }

        private static async Task FillPeronsAsync(CarContext context)
        {
            var personsData = await File.ReadAllTextAsync("TestData/Persons.json");
            var persons = JsonSerializer.Deserialize<IEnumerable<Person>>(personsData);
            context.Persons.AddRange(persons);
            await context.SaveChangesAsync();
        }

        private static async Task FillCarMakesAsync(CarContext context)
        {
            var carMakeData = await File.ReadAllTextAsync("TestData/CarMakes.json");
            var carMakes = JsonSerializer.Deserialize<IEnumerable<CarMake>>(carMakeData);
            carMakes = carMakes
                .Select(m => m.Make)
                .Distinct()
                .Select(m => new CarMake { Make = m });
            context.CarMakes.AddRange(carMakes);
            await context.SaveChangesAsync();
        }

        private static async Task FillCarModelsAsync(CarContext context)
        {
            var carModeldata = await File.ReadAllTextAsync("TestData/CarModels.json");
            var carModels = JsonSerializer.Deserialize<IEnumerable<CarModel>>(carModeldata);
            var carMakes = JsonSerializer.Deserialize<IEnumerable<CarMake>>(carModeldata).ToArray();

            for (var i = 0; i < carModels.Count(); i++)
            {
                var make = context.CarMakes.FirstOrDefault(m => m.Make == carMakes.Skip(i).First().Make);

                if (make == null)
                {
                    context.CarMakes.Add(carMakes.Skip(i).First());
                    await context.SaveChangesAsync();
                    make = await context.CarMakes.FirstOrDefaultAsync(m => m.Make == carMakes.Skip(i).First().Make);
                }

                carModels.Skip(i).First().CarMake = make;
                context.CarModels.Add(carModels.Skip(i).First());
            }

            await context.SaveChangesAsync();
        }

        private static async Task FillOwnershipsAsync(CarContext context)
        {
            var persons = await context.Persons.ToArrayAsync();
            var carModels = await context.CarModels.ToArrayAsync();
            var ownershipsdata = await File.ReadAllTextAsync("TestData/Ownerships.json");
            var ownerships = JsonSerializer.Deserialize<IEnumerable<Ownership>>(ownershipsdata).ToArray();

            var rand = new Random();
            for (var i = 0; i < ownerships.Count(); i++)
            {
                Ownership dbOwnership = new Ownership
                {
                    PersonID = persons.Skip(i).First().PersonID,
                    CarModelID = carModels[rand.Next(carModels.Length)].CarModelID,
                    VehicleIdentificationNumber = ownerships.Skip(i).First().VehicleIdentificationNumber
                };
                context.Ownerships.Add(dbOwnership);
            }

            await context.SaveChangesAsync();
        }
    }
}
