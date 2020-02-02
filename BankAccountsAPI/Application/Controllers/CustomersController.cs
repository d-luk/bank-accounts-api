using BankAccountsAPI.Application.Responses;
using BankAccountsAPI.Domain.Entities;
using BankAccountsAPI.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BankAccountsAPI.Application.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public sealed class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository customerRepository;
        private readonly IAccountRepository accountRepository;
        private readonly ITransactionRepository transactionRepository;

        public CustomersController(
            ICustomerRepository customerRepository,
            IAccountRepository accountRepository,
            ITransactionRepository transactionRepository
        )
        {
            this.customerRepository = customerRepository;
            this.accountRepository = accountRepository;
            this.transactionRepository = transactionRepository;
        }

        [HttpGet("{id}")]
        public ActionResult<CustomerResponse> Get(int id)
        {
            var customer = customerRepository.Find(id);

            if (customer == null)
            {
                return NotFound(new ErrorResponse("CUSTOMER_NOT_FOUND", $"Customer {id} does not exist"));
            }

            return CreateResponse(customer);
        }

        private CustomerResponse CreateResponse(Customer customer)
        {
            var accounts = accountRepository.FindForCustomer(customer.ID);

            var accountResponses = accounts.Select(account =>
            {
                var transactions = transactionRepository.FindForAccount(account.ID);
                var transactionResponses = transactions.Select(transaction => new CustomerResponse.Account.Transaction(transaction.Value.Amount));

                return new CustomerResponse.Account(account.ID, transactionResponses.ToArray());
            });

            return new CustomerResponse(
                customer.ID,
                customer.Name,
                customer.Surname,
                accountResponses.ToArray()
            );
        }
    }
}
