using System;

namespace BankAccountsAPI.Domain.Models
{
    public sealed class Transaction
    {
        public int ID { get; }
        public int AccountID { get; }
        public int EuroCents { get; }

        public Transaction(int id, int accountID, int euroCents)
        {
            if (euroCents == 0)
            {
                throw new ArgumentException("A transaction cannot have a value of 0 euro cents");
            }

            ID = id;
            AccountID = accountID;
            EuroCents = euroCents;
        }
    }
}
