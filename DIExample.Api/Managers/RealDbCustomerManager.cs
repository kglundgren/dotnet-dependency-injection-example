using DIExample.Api.Models;
using Utf8Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DIExample.Api.Helpers;

namespace DIExample.Api.Managers
{
    public class RealDbCustomerManager : ICustomerManager
    {
        private readonly ICustomerManager CustomerManager;
        private readonly DbContext Database;
        private List<Customer> Customers;

        public RealDbCustomerManager(ICustomerManager customerManager)
        {
            CustomerManager = customerManager;
            Database = new DbContext();
            SetupDb();
            Customers = FetchCustomers();
        }

        public List<Customer> GetAll()
        {
            return Customers;
        }

        public Customer GetCustomer()
        {
            var rng = new Random();
            return Customers[rng.Next(0, Customers.Count - 1)];
        }

        private void SetupDb()
        {
            if (File.Exists(Database.FullPath)) return;

            var customers = CustomerGenerator.Generate();
            var json = JsonSerializer.Serialize(customers);
            using (var fs = File.Create(Database.FullPath)) fs.Write(json);
        }

        private List<Customer> FetchCustomers()
        {
            List<Customer> customers;
            using (var fs = File.OpenRead(Database.FullPath))
            using (var reader = new StreamReader(fs)) {
                var json = reader.ReadToEnd();
                customers = JsonSerializer.Deserialize<List<Customer>>(json);
            }
            return customers;
        }
    }
}
