import React, { useEffect, useState } from 'react';
import './App.css';
import 'normalize.css';

const App = () => {
  return (
    <main>
      <header>
        <h1 className='customer-heading'>
          Customer <strong>John Doe</strong>
        </h1>

        <div className='total-balance'>Total balance: 100.000</div>
      </header>

      <div className='accounts'>

        <h2 className='accounts-heading'>
          Accounts
          <button className='create-account-button'>Create account</button>
        </h2>

        <div className='account'>
          <div className='current-balance'>
            <div>Current balance</div>
            <div className='current-balance__value'>30.000</div>
          </div>

          <div>
            <h3 className='transactions-heading'>Transactions</h3>

            <div>
              <div className='transaction transaction--negative'>- 100.000</div>
              <div className='transaction transaction--positive'>+ 130.000</div>
            </div>
          </div>
        </div>
      </div>
    </main>
  );

  const [customer, setCustomer] = useState<any>(null);

  useEffect(() => {
    fetch('https://localhost:32772/customers/0')
      .then(result => result.json())
      .then(customer => setCustomer(customer))
      .catch(error => {
        alert('Error!');
        console.error(error);
      })
  }, []);

  if (customer === null) {
    return (
      <div>Loading...</div>
    );
  }

  const accounts = customer.accounts.map((account: any) => {
    const transactions = account.transactions.map((transaction: any) => {
      return (
        <div>{transaction.value}</div>
      );
    });

    return (
      <div>
        <div>Balance: {account.balance}</div>
        <div>Transactions: {transactions}</div>
      </div>
    );
  });

  return (
    <div>
      <div>
        <h1>{customer.name} {customer.surname}</h1>
        <span>Total balance: {customer.totalBalance}</span>
      </div>

      <div>
        <h2>Current accounts</h2>

        {accounts}
      </div>
    </div>
  );
}

export default App;
