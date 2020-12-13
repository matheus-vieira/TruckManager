using Microsoft.EntityFrameworkCore;

namespace TruckManager.Ui.TruckData
{
    public partial class TruckContext : DbContext
    {
        public TruckContext(DbContextOptions<TruckContext> options) : base(options)
        {
        }
    }
}
