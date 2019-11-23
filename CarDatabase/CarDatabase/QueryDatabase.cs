using CarDatabase.Data;
using CarDatabase.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDatabase
{
    partial class Program
    {
        private static async Task<IEnumerable<CarMake>> QueryCarMakes(CarContext context)
        {
            return await context.CarMakes
                .Include(cmk => cmk.CarModels)
                .ToArrayAsync();
        }

        private static async Task<IEnumerable<CarModel>> QueryCarModels(CarContext context)
        {
            return await context.CarModels
                .Include(cm => cm.CarMake)
                .Include(cm => cm.Ownerships)
                .ToArrayAsync();
        }

        private static async Task<IEnumerable<Person>> QueryPersonsModels(CarContext context)
        {
            return await context.Persons
                .Include(p => p.Ownerships)
                .ToArrayAsync();
        }

        private static async Task<IEnumerable<Ownership>> QueryOwnershipsModels(CarContext context)
        {
            return await context.Ownerships
                .Include(i => i.CarModel)
                .Include(i => i.Person)
                .ToArrayAsync();
        }

        private static async Task PrintStatistics(CarContext context)
        {
            var carModels = await context.CarModels.Include(cm => cm.CarMake).ToArrayAsync();

            foreach (var model in carModels)
            {
                var ownershipsPerModel = context.Ownerships.Where(o => o.CarModel.Model == model.Model).Count();
                Console.WriteLine("For " + model.CarMake.Make + " " + model.Model + " " + ownershipsPerModel + " ownerships where created");
            }

            var modelsWithoutOwn = context.CarModels.Include(cm => cm.Ownerships).Where(cm => cm.Ownerships.Count == 0);

            if (modelsWithoutOwn.Count() == 0)
            {
                Console.WriteLine("\nFor every model a ownership was created");
            }
            else
            {
                Console.WriteLine("\nFor the following models no ownerships were created: ");
                foreach (var model in modelsWithoutOwn)
                {
                    Console.WriteLine(model.Model);
                }
            }
        }
    }
}
