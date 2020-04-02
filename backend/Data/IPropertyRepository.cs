using backend.DTO;
using backend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend.Data
{
    public interface IPropertyRepository
    {
        Task<IEnumerable<Property>> GetProperties();
        Task<Property> GetGuestProperty(int id);
        Task<Property> GetBuyerProperty(int id);
        Task<Property> GetAgentProperty(int id);
    }
}