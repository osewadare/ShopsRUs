using System;
using System.ComponentModel.DataAnnotations;

namespace ShopsRus.Core.Entities
{
    public class BaseEntity
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        public DateTime? DateModified { get; set; }
    }
}
