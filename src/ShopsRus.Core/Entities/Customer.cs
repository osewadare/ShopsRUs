using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopsRus.Core.Entities
{
    public class Customer: BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(15)]
        public string MobileNumber { get; set; }

        [Required]
        public CustomerType CustomerType { get; set; }

        public List<Invoice> Invoices { get; set; }

    }
}
