using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Linq;
using TruckManager.Ui.TruckData;
using TruckManager.Ui.TruckData.Models;

namespace TruckManager.Ui.Tests
{
    public class TestDataDbInitializer
    {
        public TruckContext Context { get; internal set; }

        public TestDataDbInitializer()
        {
            var DbContextOptions = new DbContextOptionsBuilder<TruckContext>()
                   .UseInMemoryDatabase("InMemoryDb")
                   .Options;
            Context = new TruckContext(DbContextOptions);
            Seed(Context);
        }

        public static void Seed(TruckContext context)
        {
            SeedTruckModel(context);
            SeedTruck(context);
        }
        public static void SeedTruckModel(TruckContext context)
        {
            context.TruckModels.AddRange(new TruckModel[]
            {
                new TruckModel { Model = "FH", ModelYear = GetYear() },
                new TruckModel { Model = "FM", ModelYear = GetYear() }
            });
            context.SaveChanges();
        }

        public static void SeedTruck(TruckContext context)
        {
            for (int i = 0; i < 500; i++)
                context.Trucks.Add(new Truck
                {
                    Name = "Dummy Seed " + i,
                    Year = GetYear(),
                    Model = GetRandomModel(context)
                });
            context.SaveChanges();
        }

        public static readonly Random random = new Random();
        public static readonly int minimumYear = DateTime.Today.Year;
        public static readonly int maximumYear = DateTime.Today.Year + 2;

        public static string GetYear() => new DateTime(random.Next(minimumYear, maximumYear), 1, 1).Year.ToString();

        private static TruckModel GetRandomModel(TruckContext context)
        {
            return context.TruckModels.OrderBy(o => Guid.NewGuid()).First();
        }
    }
}
