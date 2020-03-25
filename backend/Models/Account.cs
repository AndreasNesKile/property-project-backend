using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class Account
    {
        public string Id { get; set; }

        [Column(TypeName= "nvarchar(30)")]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(40)")]
        public string Surname { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string Email { get; set; }

        public DateTime DateOfBirth { get; set; }

        [Required]
        public Boolean Active { get; set; }

        [Required]
        public AccountType AccountType { get; set; }

        [Required]
        public int AccountTypeId { get; set; }

    }
}
