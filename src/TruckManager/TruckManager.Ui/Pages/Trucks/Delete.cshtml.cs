using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TruckManager.Ui.TruckData;
using TruckManager.Ui.TruckData.Models;

namespace TruckManager.Ui.Pages.Trucks
{
    public class DeleteModel : PageModel
    {
        private readonly TruckManager.Ui.TruckData.TruckContext _context;
        private readonly ILogger<DeleteModel> _logger;

        public DeleteModel(TruckManager.Ui.TruckData.TruckContext context,
            ILogger<DeleteModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public Truck Truck { get; set; }
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            Truck = await _context.Trucks
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (Truck == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ErrorMessage = "Delete failed. Try again";
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Truck = await _context.Trucks.FindAsync(id);

            try
            {
                _context.Trucks.Remove(Truck);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "error on deleting a truck");

                return RedirectToAction("./Delete",
                                     new { id, saveChangesError = true });
            }
        }
    }
}
