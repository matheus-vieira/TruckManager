using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using Moq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckManager.Ui.TruckData.Models;
using Xunit;

namespace TruckManager.Ui.Tests.Pages.Trucks
{
    public class CreateModelTests
    {
        readonly Ui.Pages.Trucks.CreateModel pageModel;
        readonly TruckData.TruckContext context;

        public CreateModelTests()
        {
            context = new TestDataDbInitializer().Context;

            pageModel = new Ui.Pages.Trucks.CreateModel(context);
        }

        [Fact]
        public void OnGet_Truck_prop_is_null()
        {
            //arrange

            //act                
            var result = pageModel.OnGet();

            //assert
            Assert.IsType<PageResult>(result);
            Assert.Null(pageModel.Truck);
        }

        [Fact]
        public async Task OnPostAsync_IfValidModel_ReturnPage()
        {
            //Arrange
            pageModel.Truck = new Truck
            {
                Name = "Test Truck",
                ModelId = context.TruckModels.OrderBy(o => Guid.NewGuid()).First().Id,
                Year = "2020"
            };

            //Act
            var result = await pageModel.OnPostAsync();

            //Assert
            Assert.IsType<RedirectToPageResult>(result);
            var truckSaved = context.Trucks.First(t => t.Name == pageModel.Truck.Name);
            Assert.NotNull(truckSaved);
        }

        [Fact]
        public async Task OnPostAsync_IfInvalidModel_ReturnPage()
        {
            //Arrange
            pageModel.Truck = new Truck
            {
                Name = null,
                ModelId = Guid.Empty,
                Year = null
            };

            pageModel.ModelState.Clear();
            pageModel.ModelState.SetModelValue(nameof(Truck), pageModel.Truck, "");

            //Act
            var result = await pageModel.OnPostAsync();

            //Assert
            Assert.IsType<PageResult>(result);
        }
    }
}
