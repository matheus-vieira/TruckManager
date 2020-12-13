using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckManager.Ui.TruckData.Models;
using Xunit;

namespace TruckManager.Ui.Tests.Pages.Trucks
{
    public class EditModelTest
    {
        readonly Ui.Pages.Trucks.EditModel pageModel;
        readonly TruckData.TruckContext context;

        public EditModelTest()
        {
            context = new TestDataDbInitializer().Context;

            pageModel = new Ui.Pages.Trucks.EditModel(context);
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
            Assert.NotEmpty(pageModel.TruckModelsSelectList);
            Assert.NotNull(pageModel.Truck);
            Assert.NotNull(pageModel.Truck.Name);
            Assert.NotNull(pageModel.Truck.Year);
            Assert.NotNull(pageModel.Truck.Model);
            Assert.NotNull(pageModel.Truck.Model.Model);
            Assert.NotNull(pageModel.Truck.Model.ModelYear);
        }

        [Fact]
        public async Task OnPostAsync_IfInvalidModel_ReturnPage()
        {
            //Arrange
            var id = context.Trucks.OrderBy(o => Guid.NewGuid()).First().Id;
            await pageModel.OnGetAsync(id);

            //act
            pageModel.ModelState.Clear();
            pageModel.ModelState.SetModelValue(nameof(Truck), pageModel.Truck, "");

            //Act
            var result = await pageModel.OnPostAsync();

            //Assert
            Assert.IsType<PageResult>(result);
        }

        [Fact]
        public async Task OnPostAsync_IfValidModel_ReturnPage()
        {
            //Arrange
            var id = context.Trucks.OrderBy(o => Guid.NewGuid()).First().Id;
            await pageModel.OnGetAsync(id);

            pageModel.Truck.Name = "Edited";

            //Act
            var result = await pageModel.OnPostAsync();

            //Assert
            Assert.IsType<RedirectToPageResult>(result);
            var truckSaved = context.Trucks.First(t => t.Name == pageModel.Truck.Name);
            Assert.NotNull(truckSaved);
            Assert.Equal(pageModel.Truck.Name, truckSaved.Name);
        }
    }
}
