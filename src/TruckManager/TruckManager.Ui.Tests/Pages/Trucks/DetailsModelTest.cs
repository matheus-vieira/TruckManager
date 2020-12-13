using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TruckManager.Ui.Tests.Pages.Trucks
{
    public class DetailsModelTest
    {
        readonly Ui.Pages.Trucks.DetailsModel pageModel;
        readonly TruckData.TruckContext context;

        public DetailsModelTest()
        {
            context = new TestDataDbInitializer().Context;

            pageModel = new Ui.Pages.Trucks.DetailsModel(context);
        }

        [Fact]
        public async Task OnGet_Truck_id_is_null()
        {
            //arrange

            //act                
            var result = await pageModel.OnGetAsync(null);

            //assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task OnGet_Truck_id_is_guid_empty()
        {
            //arrange
            var id = Guid.Empty;

            //act                
            var result = await pageModel.OnGetAsync(id);

            //assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task OnGet_Truck_id_is_guid_not_in_db()
        {
            //arrange
            var id = Guid.NewGuid();

            //act                
            var result = await pageModel.OnGetAsync(id);

            //assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task OnGet_Truck_id_is_guid_in_db()
        {
            //arrange
            var id = context.Trucks.OrderBy(o => Guid.NewGuid()).First().Id;

            //act                
            var result = await pageModel.OnGetAsync(id);

            //assert
            Assert.IsType<PageResult>(result);
            Assert.NotNull(pageModel.Truck);
            Assert.NotNull(pageModel.Truck.Name);
            Assert.NotNull(pageModel.Truck.Year);
            Assert.NotNull(pageModel.Truck.Model);
            Assert.NotNull(pageModel.Truck.Model.Model);
            Assert.NotNull(pageModel.Truck.Model.ModelYear);
        }
    }
}
