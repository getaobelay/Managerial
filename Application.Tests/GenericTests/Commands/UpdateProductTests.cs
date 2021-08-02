using FluentAssertions;
using System.Threading.Tasks;
using NUnit.Framework;
using System;
using Application.Common.Commands.Requests;
using Application.ViewModels;
using Domain.Entites;
using FluentValidation;
using Application.UnitTests;

namespace Application.UnitTests.GenericTests.Commands
{
    using static Testing;

    public class UpdateProductTests : TestBase
    {
        [Test]
        public void ShouldRequireValidTodoItemId()
        {
            var command = new UpdateCommandRequest<Product, ProductViewModel>();

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldUpdateTodoItem()
        {
            var userId = await RunAsDefaultUserAsync();

            var product = await SendAsync(new CreateCommandRequest<Product, ProductViewModel>
            {
                CreateObject = new ProductViewModel
                {
                    Name = "new product"
                }
            });

            var command = new UpdateCommandRequest<Product, ProductViewModel>()
            {
                Id = product.ViewModel.Id,
                UpdatedObject = new ProductViewModel()
                {
                    Id = product.ViewModel.Id,
                    Name = "updated product"
                }
            };

            await SendAsync(command);

            var item = await FindAsync<ProductViewModel>(product);

            item.Name.Should().Be(command.UpdatedObject.Name);
            item.UpdatedBy.Should().NotBeNull();
            item.UpdatedBy.Should().Be(userId);
            item.UpdatedDate.Should().BeCloseTo(DateTime.Now, 10000);
        }
    }
}
