namespace BankAccountsAPI.Domain.Models
{
    public sealed class Account
    {
        public int ID { get; }
        public int CustomerID { get; }

        public Account(int id, int customerID)
        {
            ID = id;
            CustomerID = customerID;
        }
    }
}
