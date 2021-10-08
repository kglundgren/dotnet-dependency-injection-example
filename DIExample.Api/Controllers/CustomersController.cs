using DIExample.Api.Managers;
using DIExample.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DIExample.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ILogger<CustomersController> Logger;
        private readonly ICustomerManager CustomerManager;

        public CustomersController(ILogger<CustomersController> logger, ICustomerManager customerManager)
        {
            Logger = logger;
            CustomerManager = customerManager;
        }

        // GET api/<CustomersController>/5
        [HttpGet("{id}")]
        public Customer Get(int id)
        {
            return CustomerManager.GetCustomer(id);
        }

        // GET api/<CustomersController>
        [HttpGet]
        public List<Customer> GetCustomers()
        {
            return CustomerManager.GetAll();
        }

        // POST api/<CustomersController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CustomersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CustomersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
