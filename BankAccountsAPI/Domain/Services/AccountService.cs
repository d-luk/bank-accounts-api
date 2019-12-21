using BankAccountsAPI.Domain.Exceptions;
using BankAccountsAPI.Domain.Models;
using BankAccountsAPI.Domain.Repositories;
using System;

namespace BankAccountsAPI.Domain.Services
{
    public sealed class AccountService
    {
        private readonly ICustomerRepository customerRepository;
        private readonly IUnitOfWork unitOfWork;

        public AccountService(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
        {
            this.customerRepository = customerRepository;
            this.unitOfWork = unitOfWork;
        }

        public Account Create(int customerID, int initialCreditEuroCents = 0)
        {
            if (!customerRepository.Exists(customerID))
            {
                throw new ModelNotFoundException($"Customer {customerID} does not exist");
            }

            using var unitOfWork = this.unitOfWork;

            var account = unitOfWork.AccountRepository.Create(customerID);

            if (initialCreditEuroCents != 0)
            {
                unitOfWork.TransactionRepository.Create(account.ID, initialCreditEuroCents);
            }

            unitOfWork.Commit();

            return account;
        }

        /// <summary>
        ///     Allows using repositories in a transaction.
        /// </summary>
        public interface IUnitOfWork : IDisposable
        {
            public IAccountRepository AccountRepository { get; }
            public ITransactionRepository TransactionRepository { get; }

            public void Commit();
        }
    }
}
