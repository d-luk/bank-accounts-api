using BankAccountsAPI.Domain.ValueObjects;
using Xunit;

namespace Tests.Unit.Domain.ValueObjects
{
    public sealed class MoneyTest
    {
        [Fact]
        public void ItEquals()
        {
            Assert.True(new Money(0m) == new Money(0m));
            Assert.True(new Money(100m) == new Money(100m));
            Assert.True(new Money(-10m) == new Money(-10m));

            Assert.False(new Money(0m) == new Money(1m));
            Assert.False(new Money(1m) == new Money(0m));
            Assert.False(new Money(-10m) == new Money(10m));
        }

        [Fact]
        public void ItDoesNotEqual()
        {
            Assert.False(new Money(0m) != new Money(0m));
            Assert.False(new Money(100m) != new Money(100m));
            Assert.False(new Money(-10m) != new Money(-10m));

            Assert.True(new Money(0m) != new Money(1m));
            Assert.True(new Money(1m) != new Money(0m));
            Assert.True(new Money(-10m) != new Money(10m));
        }
    }
}
