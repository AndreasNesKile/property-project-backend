using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.DTO
{
    public class PropertyToListDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Line_1 { get; set; } // Street Address
        public string Line_2 { get; set; } // Apartment Number
        public string City { get; set; }
        public PropertyImageDTO PropertyImage { get; set; }
        public string PropertyStatus { get; set; }
        public string PropertyType { get; set; }
    }
}
