using System;

namespace BankAccountsAPI.Domain.Exceptions
{
    public sealed class ModelNotFoundException : Exception
    {
        public ModelNotFoundException() { }
        public ModelNotFoundException(string message) : base(message) { }
        public ModelNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }
}
