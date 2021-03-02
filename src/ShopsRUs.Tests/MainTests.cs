using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Moq;
using ShopRUs.Services.Interfaces.Interfaces;
using ShopsRus.Core;
using ShopsRus.Core.Entities;
using ShopsRus.Core.Models;
using ShopsRUs.Infrastructure;
using ShopsRUs.Services.Services;
using Xunit;

namespace ShopsRUs.Tests
{
    public class MainTests
    {
        private Mock<IRepository<Invoice>> mockInvoiceRepo = new Mock<IRepository<Invoice>>();
        private Mock<IRepository<Customer>> mockCustomerRepo = new Mock<IRepository<Customer>>();
        private Mock<ILogger<InvoiceService>> mockLog = new Mock<ILogger<InvoiceService>>();

        [Fact]
        public void StandardUserOlderThan2YearsGetsPercentageDiscount()
        {
            //arrange

            var items = new List<Item>
            {
                new Item
                {
                    Amount = "50",
                    Category = "Books",
                    Name = "Atomic Habits"
                }
            };

            var customer = new Customer
            {
                FirstName = "Dare",
                LastName = "Osewa",
                DateCreated = DateTime.Now.AddYears(-3),
                CustomerType = CustomerType.Standard,
                MobileNumber = "08160613889"
            };

            mockCustomerRepo.Setup(x => x.Get(1)).Returns(customer);

            var invoiceService = new InvoiceService(mockInvoiceRepo.Object, mockCustomerRepo.Object, mockLog.Object);

            //act
            var result = invoiceService.ComputeInvoiceAmount(items, "1");

            //assert
            Assert.Equal("47.50", result);
        }

        [Fact]
        public void NewStandardUserGetsNoPercentageDiscount()
        {
            //arrange

            var items = new List<Item>
            {
                new Item
                {
                    Amount = "100",
                    Category = "Books",
                    Name = "Atomic Habits"
                }
            };

            var customer = new Customer
            {
                FirstName = "Dare",
                LastName = "Osewa",
                DateCreated = DateTime.Now,
                CustomerType = CustomerType.Standard,
                MobileNumber = "08160613889"
            };

            mockCustomerRepo.Setup(x => x.Get(1)).Returns(customer);

            var invoiceService = new InvoiceService(mockInvoiceRepo.Object, mockCustomerRepo.Object, mockLog.Object);

            //act
            var result = invoiceService.ComputeInvoiceAmount(items, "1");

            //assert
            Assert.Equal("95", result);
        }

        [Fact]
        public void AffiliateUserGetsAffiliateAndFiveDiscount()
        {
            //arrange

            var items = new List<Item>
            {
                new Item
                {
                    Amount = "100",
                    Category = "Books",
                    Name = "Atomic Habits"
                }
            };

            var customer = new Customer
            {
                FirstName = "Dare",
                LastName = "Osewa",
                DateCreated = DateTime.Now,
                CustomerType = CustomerType.Affiliate,
                MobileNumber = "08160613889"
            };

            mockCustomerRepo.Setup(x => x.Get(1)).Returns(customer);

            var invoiceService = new InvoiceService(mockInvoiceRepo.Object, mockCustomerRepo.Object, mockLog.Object);

            //act
            var result = invoiceService.ComputeInvoiceAmount(items, "1");

            //assert
            Assert.Equal("85.0", result);
        }

        [Fact]
        public void EmployeeUserGetsEmployeeAndFiveDiscount()
        {
            //arrange

            var items = new List<Item>
            {
                new Item
                {
                    Amount = "100",
                    Category = "Books",
                    Name = "Atomic Habits"
                }
            };

            var customer = new Customer
            {
                FirstName = "Dare",
                LastName = "Osewa",
                DateCreated = DateTime.Now,
                CustomerType = CustomerType.Employee,
                MobileNumber = "08160613889"
            };

            mockCustomerRepo.Setup(x => x.Get(1)).Returns(customer);

            var invoiceService = new InvoiceService(mockInvoiceRepo.Object, mockCustomerRepo.Object, mockLog.Object);

            //act
            var result = invoiceService.ComputeInvoiceAmount(items, "1");

            //assert
            Assert.Equal("65.0", result);
        }

        [Fact]
        public void NoPercentageDiscountOnGroceries()
        {
            //arrange

            var items = new List<Item>
            {
                new Item
                {
                    Amount = "90",
                    Category = "Groceries",
                    Name = "Corn Flakes"
                }
            };

            var customer = new Customer
            {
                FirstName = "Dare",
                LastName = "Osewa",
                DateCreated = DateTime.Now,
                CustomerType = CustomerType.Affiliate,
                MobileNumber = "08160613889"
            };

            mockCustomerRepo.Setup(x => x.Get(1)).Returns(customer);

            var invoiceService = new InvoiceService(mockInvoiceRepo.Object, mockCustomerRepo.Object, mockLog.Object);

            //act
            var result = invoiceService.ComputeInvoiceAmount(items, "1");

            //assert
            Assert.Equal("90.0", result);
        }

        [Fact]
        public void GetTotalBillAmountTest()
        {
            //arrange
            var items = new List<Item>
            {
                new Item
                {
                    Amount = "50",
                    Category = "Groceries",
                    Name = "Milk"
                },
                new Item
                {
                    Amount = "100",
                    Category = "Groceries",
                    Name = "Cocopops"
                },
                 new Item
                {
                    Amount = "100",
                    Category = "Books",
                    Name = "Atomic Habits"
                },
                 new Item
                {
                    Amount = "100",
                    Category = "Books",
                    Name = "Clean Code"
                }
            };
            var invoice = new Invoice {
                CustomerId = 1,
                DateCreated = DateTime.Now,
                DiscountAmount = 50.0m,
                InvoiceAmount = 90.0m,
                Id = 1
            };

            var invoiceService = new InvoiceService(mockInvoiceRepo.Object, mockCustomerRepo.Object, mockLog.Object);

            //act
            var result = invoiceService.GetTotalBillAmount(items);

            //assert
            Assert.Equal(350.0m, result);

        }

        [Fact]
        public void GetTotalBillAmountExcludingGroceriesTest()
        {
            //arrange
            var items = new List<Item>
            {
                new Item
                {
                    Amount = "50",
                    Category = "Groceries",
                    Name = "Milk"
                },
                new Item
                {
                    Amount = "100",
                    Category = "Groceries",
                    Name = "Cocopops"
                },
                 new Item
                {
                    Amount = "100",
                    Category = "Books",
                    Name = "Atomic Habits"
                },
                 new Item
                {
                    Amount = "100",
                    Category = "Books",
                    Name = "Clean Code"
                }
            };
           
            var invoiceService = new InvoiceService(mockInvoiceRepo.Object, mockCustomerRepo.Object, mockLog.Object);

            //act
            var result = invoiceService.GetTotalBillAmountExcludingGroceries(items);

            //assert
            Assert.Equal(200.0m, result);

        }



       
    }
}
