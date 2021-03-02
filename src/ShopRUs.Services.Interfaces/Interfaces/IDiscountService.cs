using System;
using System.Collections.Generic;
using ShopsRus.Core.Entities;

namespace ShopRUs.Services.Interfaces.Interfaces
{
    public interface IDiscountService
    {
        bool CreateDiscount(Discount discount);

        List<Discount> GetAllDiscounts();

        Discount GetDiscountDetails(string Id);

    }
}
