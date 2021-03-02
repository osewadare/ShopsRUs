using System;
using System.ComponentModel.DataAnnotations;

namespace ShopsRus.Core.Entities
{
    public class Invoice: BaseEntity
    {
        [Required]
        public long CustomerId { get; set; }

        [Required]
        public decimal InvoiceAmount { get; set; }

        [Required]
        public decimal DiscountAmount { get; set; }

    }
}
