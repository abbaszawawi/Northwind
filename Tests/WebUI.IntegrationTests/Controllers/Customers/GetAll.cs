using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Northwind.Domain.Entities;
using Northwind.WebUI.IntegrationTests.Common;
using Xunit;

namespace Northwind.WebUI.IntegrationTests.Controllers.Customers
{
    public class GetAll : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> factory;

        public GetAll(WebApplicationFactory<Startup> factory) => this.factory = factory;

        [Fact]
        public async Task ReturnsCustomers()
        {
            var response = await factory.CreateClient().GetAsync("/customers");

            response.EnsureSuccessStatusCode();
            var customers = await Utilities.GetResponseContent<IEnumerable<Customer>>(response);

            Assert.IsAssignableFrom<IEnumerable<Customer>>(customers);
            Assert.NotEmpty(customers);
        }
    }
}
