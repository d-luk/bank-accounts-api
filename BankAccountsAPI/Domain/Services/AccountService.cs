using BankAccountsAPI.Domain.Exceptions;
using BankAccountsAPI.Domain.Entities;
using BankAccountsAPI.Domain.Repositories;
using System;
using BankAccountsAPI.Domain.ValueObjects;

namespace BankAccountsAPI.Domain.Services
{
    public sealed class AccountService : IAccountService
    {
        private readonly ICustomerRepository customerRepository;
        private readonly IUnitOfWork unitOfWork;

        public AccountService(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
        {
            this.customerRepository = customerRepository;
            this.unitOfWork = unitOfWork;
        }

        public Account Create(int customerID, Money? initialCredit = null)
        {
            if (!customerRepository.Exists(customerID))
            {
                throw new ModelNotFoundException($"Customer {customerID} does not exist");
            }

            using var unitOfWork = this.unitOfWork;

            var account = unitOfWork.AccountRepository.Create(customerID);

            if (initialCredit.HasValue && initialCredit.Value.Amount != 0)
            {
                unitOfWork.TransactionRepository.Create(account.ID, initialCredit.Value);
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
