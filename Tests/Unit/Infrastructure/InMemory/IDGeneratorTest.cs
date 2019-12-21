using BankAccountsAPI.Infrastructure.InMemory;
using Xunit;

namespace Tests.Unit.Infrastructure.InMemory
{
    public sealed class IDGeneratorTest
    {
        [Fact]
        public void ItGeneratesIncrementingIDsPerType()
        {
            var generator = new IDGenerator();

            Assert.Equal(0, generator.GenerateFor<object>());
            Assert.Equal(1, generator.GenerateFor<object>());

            Assert.Equal(0, generator.GenerateFor<string>());
            Assert.Equal(2, generator.GenerateFor<object>());
            Assert.Equal(1, generator.GenerateFor<string>());
        }
    }
}
