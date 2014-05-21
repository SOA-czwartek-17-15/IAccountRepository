using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Contracts;

namespace AccountRepository.DataAccess
{
    interface IAccountDataAccess
    {
        void SaveOrUpdate(Account account);
        Account FindById(Guid accountId);
        Account FindByAccountNumber(string accountNumber);
    }
}
