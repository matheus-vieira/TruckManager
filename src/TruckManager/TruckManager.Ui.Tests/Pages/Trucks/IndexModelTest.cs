using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TruckManager.Ui.Tests.Pages.Trucks
{
    public class IndexModelTest
    {
        readonly Ui.Pages.Trucks.IndexModel pageModel;
        readonly TruckData.TruckContext context;

        public IndexModelTest()
        {
            context = new TestDataDbInitializer().Context;

            pageModel = new Ui.Pages.Trucks.IndexModel(context);
        }

        [Fact]
        public async Task OnGet_Truck_sortOrder_is_null_pageIndex_is_null()
        {
            //arrange

            //act                
            await pageModel.OnGetAsync(null, null);

            //assert
            Assert.NotEmpty(pageModel.Trucks);
            Assert.True(pageModel.Trucks.Any());
            Assert.Equal(1, pageModel.Trucks.PageIndex);
            Assert.Null(pageModel.CurrentSort);
        }

        [Fact]
        public async Task OnGet_Truck_sortOrder_is_null_pageIndex_2()
        {
            //arrange

            //act                
            await pageModel.OnGetAsync(null, 2);

            //assert
            Assert.NotEmpty(pageModel.Trucks);
            Assert.True(pageModel.Trucks.Any());
            Assert.Equal(2, pageModel.Trucks.PageIndex);
            Assert.Null(pageModel.CurrentSort);
        }

        [Fact]
        public async Task OnGet_Truck_sortOrder_is_name_desc_pageIndex_2()
        {
            //arrange

            //act                
            await pageModel.OnGetAsync("name", 2);

            //assert
            Assert.NotEmpty(pageModel.Trucks);
            Assert.True(pageModel.Trucks.Any());
            Assert.Equal(2, pageModel.Trucks.PageIndex);
            Assert.Equal("name", pageModel.CurrentSort);
        }
    }
}
