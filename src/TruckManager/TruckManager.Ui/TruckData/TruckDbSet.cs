using Microsoft.EntityFrameworkCore;
using TruckManager.Ui.TruckData.Models;

namespace TruckManager.Ui.TruckData
{
    public partial class TruckContext
    {

        public DbSet<Truck> Trucks { get; set; }
        public DbSet<TruckModel> TruckModels { get; set; }
    }
}
