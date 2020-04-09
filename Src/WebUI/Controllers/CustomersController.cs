using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Northwind.Domain.Entities;
using Northwind.Persistence;

namespace Northwind.WebUI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly NorthwindDbContext db;

        public CustomersController(NorthwindDbContext context) => db = context;

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
