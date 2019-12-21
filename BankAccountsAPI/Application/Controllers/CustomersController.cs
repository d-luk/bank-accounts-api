using BankAccountsAPI.Application.Responses;
using BankAccountsAPI.Domain.Models;
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
        private readonly IAccountRepository accountsRepository;
        private readonly ITransactionRepository transactionRepository;

        public CustomersController(
            ICustomerRepository customerRepository,
            IAccountRepository accountsRepository,
            ITransactionRepository transactionRepository
        )
        {
            this.customerRepository = customerRepository;
            this.accountsRepository = accountsRepository;
            this.transactionRepository = transactionRepository;
        }

        [HttpGet("{id}")]
        public ActionResult<CustomerResponse> Get(int id)
        {
            var customer = customerRepository.Find(id);

            if (customer == null)
            {
                return NotFound(new
                {
                    Code = "CUSTOMER_NOT_FOUND",
                    Message = $"Customer {id} does not exist"
                });
            }

            return CreateResponse(customer);
        }

        private CustomerResponse CreateResponse(Customer customer)
        {
            var accounts = accountsRepository.FindForCustomer(customer.ID);

            var accountResponses = accounts.Select(account =>
            {
                var transactions = transactionRepository.FindForAccount(account.ID);
                var transactionResponses = transactions.Select(transaction => new CustomerResponse.Account.Transaction(transaction.EuroCents / 100f));

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
