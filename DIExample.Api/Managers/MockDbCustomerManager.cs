using DIExample.Api.Helpers;
using DIExample.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DIExample.Api.Managers
{
    public class MockDbCustomerManager : ICustomerManager
    {
        private List<Customer> Customers;

        public MockDbCustomerManager()
        {
            Customers = CustomerGenerator.Generate();
        }

        public List<Customer> GetAll()
        {
            return Customers;
        }

        public Customer GetCustomer(int number)
        {
            return Customers[number];
        }
    }
}
