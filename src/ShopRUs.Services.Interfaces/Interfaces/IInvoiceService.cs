using System;
using System.Collections.Generic;
using ShopsRus.Core.Models;

namespace ShopRUs.Services.Interfaces.Interfaces
{
    public interface IInvoiceService
    {
        string ComputeInvoiceAmount(List<Item> items, string customerId);
    }
}
