using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class Owner
    {
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(30)")]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(30)")]
        public string Surname { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(20)")]
        public string Phone { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(40)")]
        public string Email { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string DNumber { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public OwnerType OwnerType { get; set; }

        public int OwnerTypeId { get; set; }

        public List<OwnershipLog> OwnershipLogs { get; set; }
    }
}
