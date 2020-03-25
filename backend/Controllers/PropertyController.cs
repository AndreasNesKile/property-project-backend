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