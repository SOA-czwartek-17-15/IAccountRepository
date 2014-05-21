using System;
using System.Collections.Generic;
using System.Linq;
using Contracts;

namespace AccountRepository.DataAccess
{
    class MockAccountDataAccess : IAccountDataAccess
    {
        private List<Account> accountList = new List<Account>();
        public void SaveOrUpdate(Account account)
        {
            var currentAccount = accountList.FirstOrDefault(a => a.Id == account.Id);
            if (currentAccount != null)
                accountList.Remove(currentAccount);
            accountList.Add(account);
        }

        public Account FindById(Guid accountId)
        {
            return accountList.FirstOrDefault(a => a.Id == accountId);
        }

        public Account FindByAccountNumber(string accountNumber)
        {
            return accountList.FirstOrDefault(a => a.AccountNumber == accountNumber);
        }

        public IDisposable GetSession()
        {
            return new FakeSession();
        }

        class FakeSession : IDisposable
        {
            public void Dispose()
            {
               
            }
        }
    }
}