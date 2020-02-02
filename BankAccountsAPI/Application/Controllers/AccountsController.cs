using BankAccountsAPI.Application.Responses;
using BankAccountsAPI.Domain.Entities;
using BankAccountsAPI.Domain.Repositories;
using BankAccountsAPI.Domain.Services;
using BankAccountsAPI.Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BankAccountsAPI.Application.Controllers
{
    [ApiController]
    [Route("/customers/{customerID}/accounts")]
    public sealed class AccountsController : ControllerBase
    {
        private readonly ICustomerRepository customerRepository;
        private readonly IAccountService accountService;
        private readonly ITransactionRepository transactionRepository;

        public AccountsController(
            ICustomerRepository customerRepository,
            IAccountService accountService,
            ITransactionRepository transactionRepository
        )
        {
            this.customerRepository = customerRepository;
            this.accountService = accountService;
            this.transactionRepository = transactionRepository;
        }

        [HttpPost]
        public ActionResult<AccountResponse> Create(int customerID, decimal initialCredit = 0)
        {
            if (!customerRepository.Exists(customerID))
            {
                return NotFound(new ErrorResponse(
                    "CUSTOMER_NOT_FOUND",
                    $"Customer {customerID} does not exist"
                ));
            }

            var account = accountService.Create(customerID, new Money(initialCredit));

            return CreateResponse(account);
        }

        private AccountResponse CreateResponse(Account account)
        {
            var transactions = transactionRepository.FindForAccount(account.ID);
            var transactionResponses = transactions.Select(transaction => new AccountResponse.Transaction(transaction.Value.Amount));

            return new AccountResponse(account.ID, account.CustomerID, transactionResponses.ToArray());
        }
    }
}
