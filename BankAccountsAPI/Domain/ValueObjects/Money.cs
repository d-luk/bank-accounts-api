namespace BankAccountsAPI.Domain.ValueObjects
{
    public struct Money
    {
        public decimal Amount { get; }

        public Money(decimal amount)
        {
            Amount = amount;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || !GetType().Equals(obj.GetType()))
            {
                return false;
            }

            var other = (Money) obj;

            return Amount == other.Amount;
        }

        public static bool operator ==(Money left, Money right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Money left, Money right)
        {
            return !(left == right);
        }

        public override int GetHashCode()
        {
            return Amount.GetHashCode();
        }
    }
}
