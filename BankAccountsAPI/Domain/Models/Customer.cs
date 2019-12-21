namespace BankAccountsAPI.Domain.Models
{
    public sealed class Customer
    {
        public int ID { get; }
        public string Name { get; }
        public string Surname { get; }

        public Customer(int id, string name, string surname)
        {
            ID = id;
            Name = name;
            Surname = surname;
        }
    }
}
