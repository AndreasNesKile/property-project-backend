using backend.DTO;
using backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Data
{
    public class AccountRepository : IAccountRepository
    {
        private readonly PropertyDbContext _context;
        public AccountRepository(PropertyDbContext context)
        {
            _context = context;
        }
        
        public async Task<Account> GetAccount(string accountId)
        {
            return await _context.Accounts.Where(account => account.Id == accountId).Include(t => t.AccountType).FirstOrDefaultAsync();
        }

        public async Task<Account> GetAccountByEmail(string email)
        {
            return await _context.Accounts.Where(account => account.Email == email).Include(x => x.AccountType).FirstOrDefaultAsync();
        }

        public async Task<Account> CreateAccount(Account account)
        {
            await _context.Accounts.AddAsync(account);
            await _context.SaveChangesAsync();

            return account;
        }

        public async Task<AccountType> GetAccountTypeByName(string accountTypeName)
        {
            return await _context.AccountTypes.Where(x => x.Name == accountTypeName).FirstOrDefaultAsync();
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
