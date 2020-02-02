# Bank accounts API
[![Build status](https://github.com/d-luk/bank-accounts-api/workflows/Build/badge.svg)](https://github.com/d-luk/bank-accounts-api/actions)

A small web API for experimenting with ASP.NET Core.

## Starting the server
Use the following command in the root directory of the repository to run the application from source:

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
- `initialCredit` *(optional)*: Adds an initial balance to the account.
