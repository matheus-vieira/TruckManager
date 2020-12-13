using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TruckManager.Ui.TruckData.Models
{
    public class Truck
    {
        public Guid Id { get; set; }

        public string Year { get; set; }

        public TruckModel Model { get; set; }
    }
}
