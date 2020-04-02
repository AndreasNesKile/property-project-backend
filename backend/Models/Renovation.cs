using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class Renovation
    {
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        public string Description { get; set; }
        [Required]
        public DateTime DateFrom { get; set; }
        [Required]
        public DateTime DateTo { get; set; }
        [Required]
        public Property Property { get; set; }
        [Required]
        public int PropertyId { get; set; }
    }
}
