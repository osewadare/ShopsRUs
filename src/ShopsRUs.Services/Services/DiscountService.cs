using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using ShopRUs.Services.Interfaces.Interfaces;
using ShopsRus.Core.Entities;
using ShopsRUs.Infrastructure;

namespace ShopsRUs.Services.Services
{
    public class DiscountService: IDiscountService
    {
        private readonly IRepository<Discount> discountRepo;
        private readonly ILogger<DiscountService> log;

        public DiscountService(IRepository<Discount> discountRepo, ILogger<DiscountService> log)
        {
            this.discountRepo = discountRepo;
            this.log = log;
        }

        public bool CreateDiscount(Discount discount)
        {
            try
            {
                return discountRepo.Insert(discount);
            }
            catch (Exception ex)
            {
                log.LogError("DiscountService: CreateDiscount - Exception Creating Customer {ex}", ex);
                return false;

            }
        }

        public List<Discount> GetAllDiscounts()
        {
            try
            {
                var discounts = discountRepo.GetAll().ToList();
                return discounts;
            }
            catch (Exception ex)
            {
                log.LogError("DiscountService: GetAllDiscounts - Exception Getting Customers {ex}", ex);
                return null;
            }
        }

        public Discount GetDiscountDetails(string Id)
        {
            try
            {
                var parsedId = Int64.Parse(Id);
                var discount = discountRepo.Get(parsedId);
                return discount;
            }
            catch (Exception ex)
            {
                log.LogError("DiscountService: GetDiscountDetails - Exception Getting Customer By Id {ex}", ex);
                return null;
            }
        }
    }
}
