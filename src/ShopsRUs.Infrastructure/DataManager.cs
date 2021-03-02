using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using ShopsRus.Core;
using ShopsRus.Core.Entities;
using ShopsRus.Infrastructure;

namespace ShopsRUs.Infrastructure
{

    public class DataManager
    {
        public static void SeedDatabase(IServiceCollection services)
        {

            var serviceProvider = services.BuildServiceProvider();
            using (var context = new ShopContext(serviceProvider.GetRequiredService<DbContextOptions<ShopContext>>()))
            {
                RelationalDatabaseCreator databaseCreator = (RelationalDatabaseCreator) context.Database.GetService<IDatabaseCreator>();
                databaseCreator.EnsureCreated();
                if (context.Customers.Any())
                 {
                     return;
                 }
                context.Customers.AddRange(
                     new Customer
                     {
                         FirstName = "Dare",
                         LastName = "Osewa",
                         DateCreated = DateTime.Now,
                         CustomerType = CustomerType.Standard,
                         MobileNumber = "08160613889"
                     },
                    new Customer
                    {
                        FirstName = "Dare",
                        LastName = "Osewa",
                        DateCreated = DateTime.Now.AddYears(-3),
                        CustomerType = CustomerType.Standard,
                        MobileNumber = "08160613889"
                    },
                    new Customer
                    {
                        FirstName = "Tunde",
                        LastName = "Osewa",
                        DateCreated = DateTime.Now,
                        CustomerType = CustomerType.Employee,
                        MobileNumber = "08160613889"
                    },
                    new Customer
                    {
                        FirstName = "Mary",
                        LastName = "Ade",
                        DateCreated = DateTime.Now.AddYears(-1),
                        CustomerType = CustomerType.Affiliate,
                        MobileNumber = "08160613887"
                    }
               );
                context.SaveChanges();

                if (context.Invoices.Any())
                {
                    return;
                }
                context.Discounts.AddRange(
                     new Discount
                     {
                         Name = "Affiliate",
                         Percentage = "10",
                         DateCreated = DateTime.Now
                     },
                    new Discount
                    {
                        Name = "Employee",
                        Percentage = "30",
                        DateCreated = DateTime.Now
                    },
                    new Discount
                    {
                        Name = "CustomerOlderThan2Years",
                        Percentage = "5",
                        DateCreated = DateTime.Now
                    });
                   context.SaveChanges();

            }
        }
    }

}
