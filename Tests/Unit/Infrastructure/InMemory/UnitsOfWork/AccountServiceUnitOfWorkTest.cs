using BankAccountsAPI.Domain.ValueObjects;
using BankAccountsAPI.Infrastructure.InMemory;
using BankAccountsAPI.Infrastructure.InMemory.UnitsOfWork;
using Xunit;

namespace Tests.Unit.Infrastructure.InMemory.UnitsOfWork
{
    public sealed class AccountServiceUnitOfWorkTest
    {
        [Fact]
        public void ItOnlyUpdatesAccountsWhenCommitted()
        {
            var database = new Database();
            var unitOfWork = new AccountServiceUnitOfWork(database);
            var customerID = 123;

            using (unitOfWork)
            {
                Assert.Empty(database.Accounts);
                Assert.Empty(unitOfWork.AccountRepository.FindForCustomer(customerID));

                unitOfWork.AccountRepository.Create(customerID);

                Assert.Empty(database.Accounts);
                Assert.Single(unitOfWork.AccountRepository.FindForCustomer(customerID));

                unitOfWork.Commit();

                Assert.Single(database.Accounts);
                Assert.Single(unitOfWork.AccountRepository.FindForCustomer(customerID));
            }

            Assert.Single(database.Accounts);
            Assert.Single(unitOfWork.AccountRepository.FindForCustomer(customerID));
        }

        [Fact]
        public void ItOnlyUpdatesTransactionsWhenCommitted()
        {
            var database = new Database();
            var unitOfWork = new AccountServiceUnitOfWork(database);

            var accountID = 123;

            using (unitOfWork)
            {
                Assert.Empty(database.Transactions);
                Assert.Empty(unitOfWork.TransactionRepository.FindForAccount(accountID));

                unitOfWork.TransactionRepository.Create(accountID, new Money(999m));

                Assert.Empty(database.Transactions);
                Assert.Single(unitOfWork.TransactionRepository.FindForAccount(accountID));

                unitOfWork.Commit();

                Assert.Single(database.Transactions);
                Assert.Single(unitOfWork.TransactionRepository.FindForAccount(accountID));
            }

            Assert.Single(database.Transactions);
            Assert.Single(unitOfWork.TransactionRepository.FindForAccount(accountID));
        }

        [Fact]
        public void ItDiscardsUncommittedChanges()
        {
            var database = new Database();
            var unitOfWork = new AccountServiceUnitOfWork(database);

            var customerID = 123;

            using (unitOfWork)
            {
                Assert.Empty(database.Accounts);
                Assert.Empty(unitOfWork.AccountRepository.FindForCustomer(customerID));

                unitOfWork.AccountRepository.Create(customerID);

                Assert.Empty(database.Accounts);
                Assert.Single(unitOfWork.AccountRepository.FindForCustomer(customerID));

                // Note: Unit of work is disposed without calling Commit()
            }

            Assert.Empty(database.Accounts);
            Assert.Empty(unitOfWork.AccountRepository.FindForCustomer(customerID));
        }
    }
}
