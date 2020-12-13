using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace TruckManager.Ui.Tests.Pages.Trucks
{
    public class DeleteModelTest
    {
        readonly Ui.Pages.Trucks.DeleteModel pageModel;
        readonly TruckData.TruckContext context;

        public DeleteModelTest()
        {
            context = new TestDataDbInitializer().Context;

            var logger = new Mock<ILogger<Ui.Pages.Trucks.DeleteModel>>().Object;

            pageModel = new Ui.Pages.Trucks.DeleteModel(context, logger);
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

        [Fact]
        public async Task OnPostAsync_id_is_null()
        {
            //Act
            var result = await pageModel.OnPostAsync(null);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task OnPostAsync_id_is_empty()
        {
            //arrange
            var id = Guid.Empty;

            //Act
            var result = await pageModel.OnPostAsync(id);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task OnPostAsync_id_is_not_in_db()
        {
            //arrange
            var id = Guid.NewGuid();

            //Act
            var result = await pageModel.OnPostAsync(id);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task OnPostAsync_id_is_in_db()
        {
            //arrange
            var id = context.Trucks.OrderBy(o => Guid.NewGuid()).First().Id;

            //Act
            var result = await pageModel.OnPostAsync(id);

            //Assert
            Assert.IsType<RedirectToPageResult>(result);
            var notExist = context.Trucks.Any(t => t.Id == id);
            Assert.False(notExist);
        }
    }
}
