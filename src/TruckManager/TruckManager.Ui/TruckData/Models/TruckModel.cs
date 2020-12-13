using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TruckManager.Ui.TruckData.Models
{
    public class TruckModel
    {
        public Guid Id { get; set; }

        [StringLength(50, ErrorMessage = "The model cannot be longer than 50 characters.")]
        public string Model { get; set; }

        [StringLength(4, MinimumLength = 4, ErrorMessage = "The truck model year cannot be longer that 4 characters")]
        public String ModelYear { get; set; }
    }
}
