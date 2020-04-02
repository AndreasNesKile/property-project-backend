using System;
using System.Collections.Generic;

namespace backend.DTO
{
    public class PropertyDetailsToAgentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
        public string Line_1 { get; set; } // Street Address
        public string Line_2 { get; set; } // Apartment Number
        public string Municipality { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string XCoordinate { get; set; }
        public string YCoordinate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastRenovated { get; set; }
        public OwnerDTO CurrentOwner { get; set; }
        public List<PropertyImageDTO> PropertyImages { get; set; }
        public List<RenovationDTO> Renovations { get; set; }
        public List<ValuationDTO> Valuations { get; set; }
        public List<OwnershipLogDTO> OwnershipLogs { get; set; }
        public int PropertyStatusId { get; set; }
        public int PropertyTypeId { get; set; }
        public string PropertyStatus { get; set; }
        public string PropertyType { get; set; }

    }
}
