using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace AccountRepository
{
    interface AccountRepository
    {
        Account getAccount(String number, int type);
        Account createAccount(int type);
    }
}
