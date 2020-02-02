using System;
using System.Collections.Generic;
using System.Linq;

namespace BankAccountsAPI.Application.Responses
{
    [Serializable]
    public sealed class AccountResponse
    {
        public int ID { get; }
        public int CustomerID { get; }
        public IEnumerable<Transaction> Transactions { get; }
        public decimal Balance { get; }

        public AccountResponse(int id, int customerID, IEnumerable<Transaction> transactions)
        {
            ID = id;
            CustomerID = customerID;
            Transactions = transactions;
            Balance = transactions.Select(transaction => transaction.Value).Sum();
        }

        [Serializable]
        public sealed class Transaction
        {
            public decimal Value { get; }

            public Transaction(decimal amount)
            {
                Value = amount;
            }
        }
    }
}
