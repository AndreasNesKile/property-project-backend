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


        /// <summary>
        /// Retrieves a account object from the SQL Db that matches the identifier.
        /// </summary>
        /// <param name="accountId">Identifier for the account.</param>
        /// <returns>If a match was found the account is returned, null if none was found</returns>
        public async Task<Account> GetAccount(string accountId)
        {
            return await _context.Accounts.Where(account => account.Id == accountId).Include(t => t.AccountType).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Retrieves a account object from the SQL Db that matches the specified email.
        /// </summary>
        /// <param name="email">Unique email of the account</param>
        /// <returns>If a match was found the account is returned, null if none was found</returns>
        public async Task<Account> GetAccountByEmail(string email)
        {
            return await _context.Accounts.Where(account => account.Email == email).Include(x => x.AccountType).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Creates a new account record by adding the specified account object to the SQL Db 
        /// </summary>
        /// <param name="account">Account object to create</param>
        /// <returns>Returns the created account if successfull</returns>
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

        /// <summary>
        /// Saves any changes made to the underlying entity instances.
        /// </summary>
        /// <returns>Returns true if any changes were saved, false i no changes were made</returns>
        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
