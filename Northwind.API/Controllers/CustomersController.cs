using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Northwind.Data;

namespace Northwind.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly NorthwindContext db;

        public CustomersController(NorthwindContext context) => db = context;

        [HttpGet]
        public IEnumerable<Customer> Get() => db.Customers.Select(c =>
            new Customer
            {
                CustomerId = c.CustomerId,
                CompanyName = c.CompanyName,
                ContactName = c.ContactName,
                ContactTitle = c.ContactTitle,
                Address = c.Address,
                City = c.City,
                Country = c.Country
            });
    }
}