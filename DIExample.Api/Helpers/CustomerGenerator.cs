using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DIExample.Api.Models;

namespace DIExample.Api.Helpers
{
    public static class CustomerGenerator
    {
        public static List<Customer> Generate()
        {
            var customers = new List<Customer>();
            for (int i = 1; i <= 10; i++) {
                customers.Add(new Customer {
                    Number = i,
                    Id = Guid.NewGuid().ToString(),
                    Name = $"Customer {i}"
                });
            }
            return customers;
        }
    }
}
