using FluentAssertions;
using System.Threading.Tasks;
using NUnit.Framework;
using Application.Common.Commands.Requests;
using Domain.Entites;
using Application.ViewModels;
using FluentValidation;
using Application.UnitTests;

namespace Application.UnitTests.GenericTests.Commands
{
    using static Testing;

    public class DeleteProductTests : TestBase
    {
        [Test]
        public void ShouldRequireProductId()
        {
            var command = new DeleteCommandRequest<Product, ProductViewModel>(){};

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldDeleteProduct()
        {
            var product = await SendAsync(new CreateCommandRequest<Product, ProductViewModel>
            {
                CreateObject = new ProductViewModel {
                    Name = "new product"
                }
            });

            await SendAsync(new DeleteCommandRequest<Product, ProductViewModel>
            {
                Id = product.ViewModel.Id
            });

            var item = await FindAsync<ProductViewModel>(product);

            item.Should().BeNull();
        }
    }
}
