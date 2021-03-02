using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using ShopsRus.Core;

namespace ShopsRUs.API.APIModels
{
    public class CreateCustomerModel
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

        public CustomerType CustomerType { get; set; } 
    }
}
