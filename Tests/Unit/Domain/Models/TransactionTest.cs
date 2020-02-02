using BankAccountsAPI.Domain.Entities;
using BankAccountsAPI.Domain.ValueObjects;
using System;
using Xunit;

namespace Tests.Unit.Domain.Models
{
    public sealed class TransactionTest
    {
        [Fact]
        public void ItMustHaveAValue()
        {
            Assert.Throws<ArgumentException>(() => new Transaction(5, 10, new Money(0m)));
        }

        [Theory]
        [InlineData(25.12)]
        [InlineData(-60.43)]
        public void ItAllowsNonZeroValues(decimal value)
        {
            var exception = Record.Exception(() => new Transaction(5, 10, new Money(value)));

            Assert.Null(exception);
        }
    }
}
