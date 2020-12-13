using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TruckManager.Ui.TruckData.Models
{
    public class TruckModel
    {
        public Guid Id { get; set; }

        public string Model { get; set; }

        public string ModelYear { get; set; }
    }
}
