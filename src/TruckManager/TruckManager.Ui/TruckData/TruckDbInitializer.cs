using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TruckManager.Ui.TruckData.Models;

namespace TruckManager.Ui.TruckData
{
    public static class TruckDbInitializer
    {
        public static void Initialize(TruckContext context)
        {
            context.Database.EnsureCreated();

            SeedTruckModel(context);
            SeedTruck(context);
        }

        private static void SeedTruck(TruckContext context)
        {
            if (context.Trucks.Any())
                return; // DB has been seeded

            for (int i = 0; i < 500; i++)
                context.Trucks.Add(new Truck
                {
                    Year = new Random().Next(minimumYear, maximumYear).ToString(),
                    Model = GetRandomModel(context)
                });

            context.SaveChanges();
        }

        private static TruckModel GetRandomModel(TruckContext context)
        {
            return context.TruckModels.OrderBy(o => Guid.NewGuid()).First();
        }

        private static readonly int minimumYear = DateTime.Today.Year;
        private static readonly int maximumYear = DateTime.Today.Year + 1;


        private static void SeedTruckModel(TruckContext context)
        {
            if (context.TruckModels.Any())
                return; // DB has been seeded

            var truckModels = new TruckModel[]
            {
                new TruckModel { Model = "FH", ModelYear = DateTime.Today.Year.ToString() },
                new TruckModel { Model = "FM", ModelYear = DateTime.Today.Year.ToString() }
            };

            foreach (var model in truckModels)
                context.TruckModels.Add(model);

            context.SaveChanges();
        }

    }
}
