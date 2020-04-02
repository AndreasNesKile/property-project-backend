using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using backend.Data;
using backend.DTO;
using backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/properties")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        private readonly IPropertyRepository _repo;
        private readonly IMapper _mapper;

        public PropertyController(IPropertyRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrives a list of all properties in the database and returns it
        /// </summary>
        /// <returns>
        /// If successful, returns 200 Ok with a list of property objects.
        /// If no properties could be found, returns 404 NotFound
        /// </returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProperties()
        {
            var properties = await _repo.GetProperties();
            if (properties == null)
            {
                return NotFound(properties);
            }
            else
            {
                var propertiesToReturn = _mapper.Map<IEnumerable<PropertyToListDTO>>(properties);
                return Ok(propertiesToReturn);
            }
        }

        /// <summary>
        /// Retrives the property that matches the specified identifier
        /// </summary>
        /// <param name="id">The id of the property that is requested</param>
        /// <returns>
        /// If successful, returns 200 Ok with a  property object.
        /// If no properties that matched the specified id could be found, returns 404 NotFound
        /// </returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProperty(int id)
        {
            Property property;

            if (User.IsInRole("Agent"))
            {
                property = await _repo.GetAgentProperty(id);
                if (property == null)
                {
                    return NotFound("Property was not found. Invalid house property Id was given.");
                }
                else
                {
                    var propertyToReturn = _mapper.Map<PropertyDetailsToAgentDTO>(property);

                    return Ok(propertyToReturn);
                }
            }
            else if (User.IsInRole("Buyer"))
            {
                property = await _repo.GetBuyerProperty(id);
                if (property == null)
                {
                    return NotFound("Property was not found. Invalid house property Id was given.");
                }
                else
                {
                    var propertyToReturn = _mapper.Map<PropertyDetailsToBuyerDTO>(property);

                    return Ok(propertyToReturn);
                }
            }
            else
            {
                property = await _repo.GetGuestProperty(id);
                if (property == null)
                {
                    return NotFound(property);
                }
                else
                {
                    var propertyToReturn = _mapper.Map<PropertyDetailsToGuestDTO>(property);

                    return Ok(propertyToReturn);
                }
            }
            
        }


    }
}