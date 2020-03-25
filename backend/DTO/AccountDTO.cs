using backend.Models;
using System;

namespace backend.DTO
{
    public class AccountDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Boolean Active { get; set; }
        public string AccountType { get; set; }
        public int AccountTypeId { get; set; }
    }
}
