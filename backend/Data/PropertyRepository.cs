using backend.DTO;
using backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Data
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly PropertyDbContext _context;

        public PropertyRepository(PropertyDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Property>> GetProperties()
        {

            var properties = await _context.Properties
                .Include(p => p.PropertyImages)
                .Include(status => status.PropertyStatus)
                .Include(type => type.PropertyType)
                .ToListAsync();

            return properties;
        }

        public async Task<Property> GetGuestProperty(int propertyId)
        {
            var result = await _context.Properties.Where(property => property.Id == propertyId)
                .Include(p => p.PropertyImages)
                .Include(status => status.PropertyStatus)
                .Include(type => type.PropertyType)
                .FirstOrDefaultAsync();

            return result;
        }

        public async Task<Property> GetBuyerProperty(int propertyId)
        {
            var result = await _context.Properties.Where(property => property.Id == propertyId)
                .Include(p => p.PropertyImages)
                .Include(r => r.Renovations)
                .Include(status => status.PropertyStatus)
                .Include(type => type.PropertyType)
                .FirstOrDefaultAsync();

            return result;
        }

        public async Task<Property> GetAgentProperty(int propertyId)
        {
            var result = await _context.Properties.Where(property => property.Id == propertyId)
                .Include(p => p.PropertyImages)
                .Include(r => r.Renovations)
                .Include(v => v.Valuations)
                .Include(o => o.OwnershipLogs)
                .Include(status => status.PropertyStatus)
                .Include(type => type.PropertyType)
                .FirstOrDefaultAsync();

            result.OwnershipLogs = await _context.OwnershipLogs.Where(log => result.OwnershipLogs.Contains(log))
                                                         .Include(log => log.Owner).Include(log => log.Owner.OwnerType).ToListAsync();
            
            return result;
        }
    }
}
