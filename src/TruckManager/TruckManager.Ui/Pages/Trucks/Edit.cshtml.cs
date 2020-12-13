using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TruckManager.Ui.TruckData;
using TruckManager.Ui.TruckData.Models;

namespace TruckManager.Ui.Pages.Trucks
{
    public class EditModel : TruckModelPageModel
    {
        private readonly TruckManager.Ui.TruckData.TruckContext _context;

        public EditModel(TruckManager.Ui.TruckData.TruckContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Truck Truck { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Truck = await _context.Trucks
                .Include(t => t.Model)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (Truck == null)
            {
                return NotFound();
            }

            PopulateTruckModelDropDownList(_context, Truck.Model.Id);
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                PopulateTruckModelDropDownList(_context, Truck.Model.Id);
                return Page();
            }

            _context.Attach(Truck).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TruckExists(Truck.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool TruckExists(Guid id)
        {
            return _context.Trucks.Any(e => e.Id == id);
        }
    }
}
