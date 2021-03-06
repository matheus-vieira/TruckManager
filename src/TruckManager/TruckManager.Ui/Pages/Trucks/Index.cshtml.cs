﻿using System;
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

        public IndexModel(TruckManager.Ui.TruckData.TruckContext context)
        {
            _context = context;
        }


        public string NameSort { get; set; }
        public string YearSort { get; set; }
        public string ModelSort { get; set; }
        public string ModelYearSort { get; set; }

        [BindProperty(SupportsGet = true)]
        public string NameFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public string YearFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public string ModelNameFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public string ModelYearFilter { get; set; }

        public string CurrentSort { get; set; }

        public PaginatedList<Truck> Trucks { get; set; }

        public async Task OnGetAsync(string sortOrder, int? pageIndex)
        {
            CurrentSort = sortOrder;
            YearSort = string.IsNullOrEmpty(sortOrder) ? "year_desc" : "";
            ModelSort = sortOrder == "model_name" ? "model_name_desc" : "model_name";
            ModelYearSort = sortOrder == "model_year" ? "model_year_desc" : "model_year";
            NameSort = sortOrder == "name" ? "name_desc" : "name";

            IQueryable<Truck> trucksIQ = from s in _context.Trucks select s;

            trucksIQ = trucksIQ.Where(s =>
            (string.IsNullOrEmpty(NameFilter) || s.Name.Contains(NameFilter)) &&
            (string.IsNullOrEmpty(YearFilter) || s.Year.Contains(YearFilter)) &&
            (string.IsNullOrEmpty(ModelNameFilter) || s.Model.Model.Contains(ModelNameFilter)) &&
            (string.IsNullOrEmpty(ModelYearFilter) || s.Model.ModelYear.Contains(ModelYearFilter)));


            trucksIQ = sortOrder switch
            {
                "year" => trucksIQ.OrderBy(s => s.Year),
                "year_desc" => trucksIQ.OrderByDescending(s => s.Year),
                "name_desc" => trucksIQ.OrderByDescending(s => s.Name),
                "model_name" => trucksIQ.OrderBy(s => s.Model.Model),
                "model_name_desc" => trucksIQ.OrderByDescending(s => s.Model.Model),
                "model_year" => trucksIQ.OrderBy(s => s.Model.ModelYear),
                "model_year_desc" => trucksIQ.OrderByDescending(s => s.Model.ModelYear),
                _ => trucksIQ.OrderBy(s => s.Name),
            };

            Trucks = await PaginatedList<Truck>.CreateAsync(
                trucksIQ.Include(t => t.Model).AsNoTracking(), pageIndex ?? 1, 10);
        }
    }
}
