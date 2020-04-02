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


        /// <summary>
        /// Retrieves a list of all the properties in the SQL database.
        /// </summary>
        /// <returns>List of properties.</returns>
        public async Task<IEnumerable<Property>> GetProperties()
        {

            var properties = await _context.Properties
                .Include(p => p.PropertyImages)
                .Include(status => status.PropertyStatus)
                .Include(type => type.PropertyType)
                .ToListAsync();

            return properties;
        }

        /// <summary>
        /// Retrieves a property object from the SQL Db that matches the identifier, with images, type and status included.
        /// </summary>
        /// <param name="propertyId">Identifier for the property.</param>
        /// <returns>If a match was found the property is returned, null if none was found</returns>
        public async Task<Property> GetGuestProperty(int propertyId)
        {
            var result = await _context.Properties.Where(property => property.Id == propertyId)
                .Include(p => p.PropertyImages)
                .Include(status => status.PropertyStatus)
                .Include(type => type.PropertyType)
                .FirstOrDefaultAsync();

            return result;
        }

        /// <summary>
        /// Retrieves a property object from the SQL Db that matches the identifier, with images, type, status, and renovations included.
        /// </summary>
        /// <param name="propertyId">Identifier for the property.</param>
        /// <returns>If a match was found the property is returned, null if none was found</returns>
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

        /// <summary>
        /// Retrieves a property object from the SQL Db that matches the identifier, with everything related to the property included.
        /// </summary>
        /// <param name="propertyId">Identifier for the property.</param>
        /// <returns>If a match was found the property is returned, null if none was found</returns>
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
