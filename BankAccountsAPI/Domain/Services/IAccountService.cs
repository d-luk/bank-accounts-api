using BankAccountsAPI.Domain.Models;

namespace BankAccountsAPI.Domain.Services
{
    public interface IAccountService
    {
        public Account Create(int customerID, int initialCreditEuroCents = 0);
    }
}
