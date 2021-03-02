using System;
using System.ComponentModel.DataAnnotations;

namespace ShopsRus.Core.Entities
{
    public class Discount: BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string Percentage { get; set; }

    }
}
