using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Northwind.API.Tests.Common;
using Northwind.Data;
using Xunit;

namespace Northwind.API.Tests.Controllers.Customers
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
