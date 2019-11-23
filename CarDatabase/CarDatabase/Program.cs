using CarDatabase.Data;
using CarDatabase.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarDatabase
{
    static partial class Program
    {
        static async Task Main(string[] args)
        {
            using var context = new CarContext();
            await CleanDatabaseAsync(context);
            await FillCarMakesAsync(context);
            await FillCarModelsAsync(context);
            await FillPeronsAsync(context);
            await FillOwnershipsAsync(context);
            printCarModels(await QueryCarModels(context));
            await PrintStatistics(context);
        }

        public static void printCarMakes(IEnumerable<CarMake> makes)
        {
            foreach (var make in makes)
            {
                Console.WriteLine(make.Make);
            }
        }

        public static void printCarModels(IEnumerable<CarModel> models)
        {
            foreach (var model in models)
            {
                Console.WriteLine(model.Model);
            }
        }
    }
}
