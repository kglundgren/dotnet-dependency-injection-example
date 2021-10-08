using System;
using System.Collections.Generic;
using System.Linq;
using DIExample.Api.Models;
using System.Threading.Tasks;

namespace DIExample.Api.Managers
{
    public interface ICustomerManager
    {
        Customer GetCustomer();
        List<Customer> GetAll();
    }
}
