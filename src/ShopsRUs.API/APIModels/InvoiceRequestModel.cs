using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ShopsRus.Core.Models;
using ShopsRUs.API.CustomValidation;

namespace ShopsRUs.API.APIModels
{
    public class InvoiceRequestModel
    {
        [Required]
        public string CustomerId { get; set; }

        [Required]
        [EnsureAtLeastOneElement]
       public List<Item> Items { get; set; }
    }
}
