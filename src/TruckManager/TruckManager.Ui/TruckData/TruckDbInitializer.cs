using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TruckManager.Ui.TruckData.Models;

namespace TruckManager.Ui.TruckData
{
    public static class TruckDbInitializer
    {
        public static void Initialize(System.IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<TruckContext>();
                context.Database.Migrate();

                SeedTruckModel(context);
                SeedTruck(context);
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred creating the Truck DB.");
            }
        }

        private static void SeedTruck(TruckContext context)
        {
            if (context.Trucks.Any())
                return; // DB has been seeded

            for (int i = 0; i < 500; i++)
                context.Trucks.Add(new Truck
                {
                    Name = "Seed " + i,
                    Year = GetYear(),
                    Model = GetRandomModel(context)
                });

            context.SaveChanges();
        }

        private static readonly Random random = new Random();
        private static readonly int minimumYear = DateTime.Today.Year;
        private static readonly int maximumYear = DateTime.Today.Year + 2;

        private static string GetYear() => new DateTime(random.Next(minimumYear, maximumYear), 1, 1).Year.ToString();

        private static TruckModel GetRandomModel(TruckContext context)
        {
            return context.TruckModels.OrderBy(o => Guid.NewGuid()).First();
        }


        private static void SeedTruckModel(TruckContext context)
        {
            if (context.TruckModels.Any())
                return; // DB has been seeded

            var truckModels = new TruckModel[]
            {
                new TruckModel { Model = "FH", ModelYear = GetYear() },
                new TruckModel { Model = "FM", ModelYear = GetYear() }
            };

            foreach (var model in truckModels)
                context.TruckModels.Add(model);

            context.SaveChanges();
        }

    }
}
