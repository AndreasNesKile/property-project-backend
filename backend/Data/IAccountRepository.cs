using backend.DTO;
using backend.Models;
using System.Threading.Tasks;

namespace backend.Data
{
    public interface IAccountRepository
    {
        Task<Account> GetAccount(string id);
        Task<Account> GetAccountByEmail(string email);
        Task<Account> CreateAccount(Account account);
        Task<AccountType> GetAccountTypeByName(string accountTypeName);
        Task<bool> SaveAll();
    }
}