# Bank accounts API
A small web API for experimenting with ASP.NET Core.

## Starting the server
To run the application from source, use the following command in the project root:

```PowerShell
dotnet run --project=BankAccountsAPI
```

The application will be accessible at [https://localhost:5001](https://localhost:5001).

## Available endpoints

 ### GET /customers/`{id}`
 Shows the customer information including its accounts and transactions.
 
### POST /customers/`{id}`/accounts
Creates a new current account for a customer.

#### Parameters
- `initialCreditEuros` *(optional)*: Adds an initial balance to the account.
