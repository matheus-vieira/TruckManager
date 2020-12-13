﻿using System;
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
    public class EditModel : PageModel
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

            Truck = await _context.Trucks.FindAsync(id);

            if (Truck == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var truckToUpdate = await _context.Trucks.FindAsync(id);

            if (truckToUpdate == null)
            {
                return NotFound();
            }

            var canUpdate = await TryUpdateModelAsync(truckToUpdate, "truck", s => s.Year);

            if (canUpdate)
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}