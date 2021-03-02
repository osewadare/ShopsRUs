using System;
using System.Collections.Generic;
using ShopsRus.Core.Entities;

namespace ShopRUs.Services.Interfaces.Interfaces
{
    public interface ICustomerService
    {
        bool CreateCustomer(Customer customer);

        List<Customer> GetAllCustomers();

        Customer GetCustomerById(string Id);

        Customer GetCustomerByName(string firstName, string lastName);
    }
}
