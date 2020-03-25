using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class Valuation
    {
        public int Id { get; set; }

        [Column(TypeName="nvarchar(500)")]
        public string Comments { get; set; }
        public int Value { get; set; }
        public DateTime ValuationDate { get; set; }

        [Required]
        public Property Property { get; set; }
        [Required]
        public int PropertyId { get; set; }
    }
}
