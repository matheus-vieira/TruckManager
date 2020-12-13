using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using TruckManager.Ui.TruckData;

namespace TruckManager.Ui.Pages.Trucks
{
    public class TruckModelPageModel : PageModel
    {
        public SelectList TruckModelsSelectList { get; set; }

        public void PopulateTruckModelDropDownList(TruckContext context, object truckModelSelected = null)
        {
            var truckModelQuery = context.TruckModels.OrderBy(m => m.Model);

            TruckModelsSelectList = new SelectList(truckModelQuery.AsNoTracking(),
                        "Id", "Model", truckModelSelected);
        }
    }
}
