using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using ShopRUs.Services.Interfaces.Interfaces;
using ShopsRus.Core;
using ShopsRus.Core.Entities;
using ShopsRus.Core.Models;
using ShopsRUs.Infrastructure;

namespace ShopsRUs.Services.Services
{
    public class InvoiceService: IInvoiceService
    {
        private readonly IRepository<Invoice> invoiceRepo;
        private readonly IRepository<Customer> customerRepo;
        private readonly ILogger<InvoiceService> log;

        public InvoiceService(IRepository<Invoice> invoiceRepo,
            IRepository<Customer> customerRepo,
            ILogger<InvoiceService> log)
        {
            this.invoiceRepo = invoiceRepo;
            this.customerRepo = customerRepo;
            this.log = log;
        }

        public string ComputeInvoiceAmount(List<Item> items, string customerId)
        {
            try
            { 
                var parsedCustomerId = long.Parse(customerId);
                var customer = customerRepo.Get(parsedCustomerId);
                if(customer != null)
                {
                    var billAmount = GetTotalBillAmount(items);
                    var billAmountLessGroceries = GetTotalBillAmountExcludingGroceries(items);

                    decimal affiliateDiscountFactor = 0.1m;
                    decimal employeeDiscountFactor = 0.3m;
                    decimal customersDiscountFactor = 0.05m;
                    var fiveDiscountUnit = 5;

                    decimal affiliateDiscount = (customer.CustomerType == CustomerType.Affiliate) ? affiliateDiscountFactor * billAmountLessGroceries : 0;
                    decimal employeeDiscount = (customer.CustomerType == CustomerType.Employee) ? employeeDiscountFactor * billAmountLessGroceries : 0;
                    var notAffiliateOrEmployee = (customer.CustomerType == CustomerType.Standard);
                    decimal customerDiscount = (DateTime.Now > (customer.DateCreated.AddYears(2)) && notAffiliateOrEmployee) ? customersDiscountFactor * billAmountLessGroceries : 0;

                    var fiveDiscountMultiple = (int) billAmount / 100;
                    var fiveDiscount = (fiveDiscountMultiple < 1) ? 0 : fiveDiscountUnit * fiveDiscountMultiple;

                    var totalDiscount = affiliateDiscount + employeeDiscount + customerDiscount + fiveDiscount;

                    var billAmountAfterDiscount = billAmount - totalDiscount;

                    var invoice = new Invoice
                    {
                        DiscountAmount = totalDiscount,
                        DateCreated = DateTime.Now,
                        InvoiceAmount = billAmountAfterDiscount
                    };

                    customer.Invoices = new List<Invoice>();

                    customer.Invoices.Add(invoice);

                    customerRepo.Update(customer);

                    return billAmountAfterDiscount.ToString();

                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                log.LogError("InvoiceService: GetInvoiceAmount - Exception Computing Invoice Amount {ex}", e);
                return null;
            }
        }

        public decimal GetTotalBillAmount(List<Item> items)
        {
            var itemAmounts = items.Select(x => decimal.Parse(x.Amount)).ToList();
            var billAmount = itemAmounts.Sum();
            return billAmount;
        }


        public decimal GetTotalBillAmountExcludingGroceries(List<Item> items)
        {
            var itemsExcludingGroceries = items.Where(x => x.Category != "Groceries").ToList();
            var itemAmounts = itemsExcludingGroceries.Select(x => decimal.Parse(x.Amount)).ToList();
            var billAmount = itemAmounts.Sum();
            return billAmount;
        }
    }
}
