
using BankAccountsAPI.Domain.Models;

namespace BankAccountsAPI.Domain.Repositories
{
    public interface ICustomerRepository
    {
        public Customer Create(string name, string surname);
        public Customer? Find(int id);
        public bool Exists(int id);
    }
}
