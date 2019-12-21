using System;
using System.Collections.Generic;
using System.Linq;

namespace BankAccountsAPI.Application.Responses
{
    [Serializable]
    public sealed class CustomerResponse
    {
        public int ID { get; }
        public string Name { get; }
        public string Surname { get; }
        public IEnumerable<Account> Accounts { get; }
        public float TotalBalanceEuros { get; }

        public CustomerResponse(int id, string name, string surname, Account[] accounts)
        {
            ID = id;
            Name = name;
            Surname = surname;
            Accounts = accounts;

            TotalBalanceEuros = accounts.Select(account => account.BalanceEuros).Sum();
        }

        [Serializable]
        public sealed class Account
        {
            public int ID { get; }
            public IEnumerable<Transaction> Transactions { get; }
            public float BalanceEuros { get; }

            public Account(int id, Transaction[] transactions)
            {
                ID = id;
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
}
