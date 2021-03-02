using System;
using System.ComponentModel.DataAnnotations;

namespace ShopsRUs.API.APIModels
{
    public class DiscountAPIModel
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string Percentage { get; set; }
    }
}
