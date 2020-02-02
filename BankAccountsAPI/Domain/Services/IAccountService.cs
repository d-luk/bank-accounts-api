using BankAccountsAPI.Domain.Entities;
using BankAccountsAPI.Domain.ValueObjects;

namespace BankAccountsAPI.Domain.Services
{
    public interface IAccountService
    {
        public Account Create(int customerID, Money? initialCredit = null);
    }
}
