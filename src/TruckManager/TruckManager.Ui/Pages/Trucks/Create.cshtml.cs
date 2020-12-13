using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TruckManager.Ui.TruckData.Models;

namespace TruckManager.Ui.Pages.Trucks
{
    public class CreateModel : TruckModelPageModel
    {
        private readonly TruckManager.Ui.TruckData.TruckContext _context;

        public CreateModel(TruckManager.Ui.TruckData.TruckContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            PopulateTruckModelDropDownList(_context);

            return Page();
        }

        [BindProperty]
        public Truck Truck { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                PopulateTruckModelDropDownList(_context, Truck.ModelId);
                return Page();
            }

            var canUpdate = await TryUpdateModelAsync(Truck, "truck",
                s => s.Name,
                s => s.Year,
                s => s.ModelId);

            if (canUpdate)
            {
                _context.Trucks.Add(Truck);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            PopulateTruckModelDropDownList(_context, Truck.Model.Id);

            return Page();

        }
    }
}
