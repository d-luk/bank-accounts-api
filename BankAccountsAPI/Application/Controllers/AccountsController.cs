using BankAccountsAPI.Application.Responses;
using BankAccountsAPI.Domain.Models;
using BankAccountsAPI.Domain.Repositories;
using BankAccountsAPI.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System;
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
        public ActionResult<AccountResponse> Create(int customerID, float initialCreditEuros = 0)
        {
            if (!customerRepository.Exists(customerID))
            {
                return NotFound(new ErrorResponse(
                    "CUSTOMER_NOT_FOUND",
                    $"Customer {customerID} does not exist"
                ));
            }

            var initialCreditEuroCents = (int) Math.Floor(initialCreditEuros * 100);
            var account = accountService.Create(customerID, initialCreditEuroCents);

            return CreateResponse(account);
        }

        private AccountResponse CreateResponse(Account account)
        {
            var transactions = transactionRepository.FindForAccount(account.ID);
            var transactionResponses = transactions.Select(transaction => new AccountResponse.Transaction(transaction.EuroCents / 100f));

            return new AccountResponse(account.ID, account.CustomerID, transactionResponses.ToArray());
        }
    }
}
