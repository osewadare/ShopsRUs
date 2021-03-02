using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ShopRUs.Services.Interfaces.Interfaces;
using ShopsRus.Core;
using ShopsRus.Core.Entities;
using ShopsRUs.Infrastructure;

namespace ShopsRUs.Services.Services
{
    public class CustomerService: ICustomerService
    {
        private readonly IRepository<Customer> customerRepo;
        private readonly ILogger<CustomerService> log;
        private readonly IConfiguration config;

        public CustomerService(IRepository<Customer> customerRepo,
            ILogger<CustomerService> log, IConfiguration config)
        {
            this.customerRepo = customerRepo;
            this.log = log;
            this.config = ConfigurationLoader.LoadConfiguration();
        }

        public bool CreateCustomer(Customer customer)
        {
            try
            {
               return customerRepo.Insert(customer);
            }
            catch (Exception ex)
            {
                log.LogError("CustomerService: CreateCustomer - Exception Creating Customer {ex}", ex);
                return false;

            }
        }

        public List<Customer> GetAllCustomers()
        {
            try
            {
                 var customers = customerRepo.GetAll().ToList();
                return customers;
            }
            catch (Exception ex)
            {
                log.LogError("CustomerService: GetAllCustomers - Exception Getting Customers {ex}", ex);
                return null;
            }
        }

        public Customer GetCustomerById(string Id)
        {
            try
            {
                var parsedId = Int64.Parse(Id);
                var customer = customerRepo.Get(parsedId);
                return customer;
            }
            catch (Exception ex)
            {
                log.LogError("CustomerService: GetCustomerById - Exception Getting Customer By Id {ex}", ex);
                return null;
            }
        }

        public Customer GetCustomerByName(string firstName, string lastName)
        {
            try
            {
                firstName = firstName.ToLower();
                lastName = lastName.ToLower();
                var selectQuery = config.GetValue<string>("Queries:SelectCustomerByName");
                var customer = customerRepo.SelectQuery(selectQuery.Replace("{firstName}", firstName).Replace("{lastName}", lastName)).FirstOrDefault();
                return customer;
            }
            catch (Exception ex)
            {
                log.LogError("CustomerService: GetCustomerByName - Exception Getting Customer By Name {ex}", ex);
                return null;
            }
        }
    }
}
