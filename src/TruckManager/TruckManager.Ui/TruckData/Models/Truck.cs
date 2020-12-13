using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TruckManager.Ui.TruckData.Models
{
    public class Truck
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters.")]
        public string Name { get; set; }

        [Required]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "The truck year cannot be longer that 4 characters")]
        public String Year { get; set; }

        public Guid ModelId { get; set; }

        public TruckModel Model { get; set; }
    }
}
