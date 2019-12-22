using System;

namespace BankAccountsAPI.Application.Responses
{
    [Serializable]
    public sealed class ErrorResponse
    {
        public string Code { get; }
        public string Message { get; }

        public ErrorResponse(string code, string message)
        {
            Code = code;
            Message = message;
        }
    }
}
