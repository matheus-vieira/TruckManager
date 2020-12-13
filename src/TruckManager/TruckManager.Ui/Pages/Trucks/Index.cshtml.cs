using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TruckManager.Ui.Helpers;
using TruckManager.Ui.TruckData;
using TruckManager.Ui.TruckData.Models;

namespace TruckManager.Ui.Pages.Trucks
{
    public class IndexModel : PageModel
    {
        private readonly TruckManager.Ui.TruckData.TruckContext _context;
        private readonly MvcOptions _mvcOptions;

        public IndexModel(TruckManager.Ui.TruckData.TruckContext context, IOptions<MvcOptions> mvcOptions)
        {
            _context = context;
            _mvcOptions = mvcOptions.Value;
        }


        public string YearSort { get; set; }
        public string ModelSort { get; set; }
        public string ModelYearSort { get; set; }
        public string YearFilter { get; set; }
        public string ModelNameFilter { get; set; }
        public string ModelYearFilter { get; set; }
        public string CurrentSort { get; set; }
        public PaginatedList<Truck> Trucks { get; set; }

        public async Task OnGetAsync(string sortOrder, string year, string modelName, string modelYear, int? pageIndex)
        {
            CurrentSort = sortOrder;
            YearSort = string.IsNullOrEmpty(sortOrder) ? "year_desc" : "";
            ModelSort = sortOrder == "model_name" ? "model_name_desc" : "model_name";
            ModelYearSort = sortOrder == "model_year" ? "model_year_desc" : "model_year";

            IQueryable<Truck> trucksIQ = from s in _context.Trucks select s;

            if (string.IsNullOrEmpty(year) &&
                string.IsNullOrEmpty(modelName) &&
                string.IsNullOrEmpty(modelYear))
            {
                pageIndex = 1;
            }


            trucksIQ = trucksIQ.Where(s =>
            (string.IsNullOrEmpty(year) || s.Year.Contains(year)) &&
            (string.IsNullOrEmpty(modelName) || s.Model.Model.Contains(modelName)) &&
            (string.IsNullOrEmpty(modelYear) || s.Model.ModelYear.Contains(modelYear)));


            trucksIQ = sortOrder switch
            {
                "year" => trucksIQ.OrderBy(s => s.Year),
                "year_desc" => trucksIQ.OrderByDescending(s => s.Year),
                "model_name_desc" => trucksIQ.OrderByDescending(s => s.Year),
                "model_year" => trucksIQ.OrderBy(s => s.Year),
                "model_year_desc" => trucksIQ.OrderByDescending(s => s.Year),
                _ => trucksIQ.OrderBy(s => s.Model.Model),
            };

            int pageSize = _mvcOptions.MaxModelBindingCollectionSize;
            Trucks = await PaginatedList<Truck>.CreateAsync(
                trucksIQ.Include(t => t.Model).AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}
