using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace backend.Models
{
    public class OwnershipLog
    {
        public int Id { get; set; }

        [Required]
        public DateTime DateAcquired { get; set; }
        public DateTime? DateSold { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public Property Property { get; set; }

        [ForeignKey("Property")]
        [Required]
        public int PropertyId { get; set; }
        [Required]
        public Owner Owner { get; set; }

        [ForeignKey("Owner")]
        [Required]
        public int OwnerId { get; set; }

    }
}
