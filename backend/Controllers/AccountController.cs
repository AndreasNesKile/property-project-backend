using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using backend.Data;
using backend.DTO;
using backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _repo;
        private readonly IMapper _mapper;

        public AccountController(IAccountRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Buyer, Agent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAccount(int id)
        {
            if(User.HasClaim(x => x.Type == ClaimTypes.NameIdentifier))
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

                var account = await _repo.GetAccount(userId);

                if (account == null) 
                {
                    var accountToCreate = new Account
                    {
                        Id = userId,
                        Email = User.HasClaim(x => x.Type == "https://property.com/email") ? User.FindFirst("https://property.com/email").Value : null,
                        Active = true,
                        AccountType = await _repo.GetAccountTypeByName(User.FindFirst("https://property.com/roles").Value)
                    };

                    var createdAccount = await _repo.CreateAccount(accountToCreate);

                    var accountToReturn = _mapper.Map<AccountDTO>(createdAccount);

                    return Ok(accountToReturn);
                }
                else
                {
                    var accountToReturn = _mapper.Map<AccountDTO>(account);
                    return Ok(accountToReturn);
                }
            }
            else
            {
                return Unauthorized("User lacks necessary credentials");
            }
        }
        
        [HttpPut("{id}")]
        [Authorize(Roles = "Buyer, Agent")]
        public async Task<IActionResult> UpdateAccount(int id, AccountForUpdateDTO accountForUpdate)
        {
            if (User.HasClaim(x => x.Type == ClaimTypes.NameIdentifier))
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

                var account = await _repo.GetAccount(userId);
                
                if(account == null)
                {
                    return NotFound("User not found in database. Call GET /account/id to register");
                }

                _mapper.Map(accountForUpdate, account);

                if(await _repo.SaveAll())
                {
                    return NoContent();
                }
                return Ok("User already updated, no changes applied");
            }
            else
            {
                return Unauthorized("User lacks necessary credentials");
            }
        }
    }
}