using BankAccountsAPI.Domain.Models;
using System;
using Xunit;

namespace Tests.Unit.Domain.Models
{
    public sealed class TransactionTest
    {
        [Fact]
        public void ItMustHaveAValue()
        {
            Assert.Throws<ArgumentException>(() => new Transaction(5, 10, 0));
        }

        [Theory]
        [InlineData(25)]
        [InlineData(-60)]
        public void ItAllowsNonZeroValues(int euroCents)
        {
            var exception = Record.Exception(() => new Transaction(5, 10, euroCents));

            Assert.Null(exception);
        }
    }
}
