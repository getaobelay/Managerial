using Application.Common.Commands.Requests;
using Application.UnitTests;
using Application.ViewModels;
using Domain.Entites;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Application.UnitTests.GenericTests.Commands
{
    using static Testing;

    public class CreateProdcuctTests : TestBase
    {
        [Test]
        public async Task ShouldCreateTodoItem()
        {
            var userId = await RunAsDefaultUserAsync();

            var product = await SendAsync(new CreateCommandRequest<Product, ProductViewModel>
            {
                CreateObject = new ProductViewModel
                {
                    Name = "new product"
                }
            });

            var item = await FindAsync<ProductViewModel>(product);

            item.Should().NotBeNull();
            item.CreatedBy.Should().Be(userId);
        }
    }
}
