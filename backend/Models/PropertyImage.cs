using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class PropertyImage
    {
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(200)")]
        public string Url { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        public string Caption { get; set; }

        [Required]
        public int PropertyId { get; set; }
        [Required]
        public Property Property { get; set; }
    }
}
