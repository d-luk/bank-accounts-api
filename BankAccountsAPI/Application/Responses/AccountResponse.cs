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
        public float BalanceEuros { get; }

        public AccountResponse(int id, int customerID, Transaction[] transactions)
        {
            ID = id;
            CustomerID = customerID;
            Transactions = transactions;

            BalanceEuros = transactions.Select(transaction => transaction.Euros).Sum();
        }

        [Serializable]
        public sealed class Transaction
        {
            public float Euros { get; }

            public Transaction(float euros)
            {
                Euros = euros;
            }
        }
    }
}
