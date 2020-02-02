using BankAccountsAPI.Domain.ValueObjects;
using System;

namespace BankAccountsAPI.Domain.Entities
{
    public sealed class Transaction
    {
        public int ID { get; }
        public int AccountID { get; }
        public Money Value { get; }

        public Transaction(int id, int accountID, Money value)
        {
            if (value.Amount == 0)
            {
                throw new ArgumentException("A transaction cannot have a value of 0");
            }

            ID = id;
            AccountID = accountID;
            Value = value;
        }
    }
}
