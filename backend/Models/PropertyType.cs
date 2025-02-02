﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class PropertyType
    {
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(40)")]
        public string Name { get; set; }
    }
}
