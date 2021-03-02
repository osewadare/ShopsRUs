using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ShopsRus.Core;

namespace ShopsRUs.API.APIModels
{
    public class CustomerAPIModel
    {

        [Required]
        [MaxLength(50)]
        public string Id { get; set; }

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
