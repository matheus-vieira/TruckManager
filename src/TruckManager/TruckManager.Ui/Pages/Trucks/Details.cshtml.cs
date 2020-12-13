using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TruckManager.Ui.TruckData;
using TruckManager.Ui.TruckData.Models;

namespace TruckManager.Ui.Pages.Trucks
{
    public class DetailsModel : PageModel
    {
        private readonly TruckManager.Ui.TruckData.TruckContext _context;

        public DetailsModel(TruckManager.Ui.TruckData.TruckContext context)
        {
            _context = context;
        }

        public Truck Truck { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
                return NotFound();

            Truck = await _context.Trucks
                .Include(t => t.Model)
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id);


            if (Truck == null)
                return NotFound();

            return Page();
        }
    }
}
