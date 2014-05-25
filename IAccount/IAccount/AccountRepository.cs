using System.ServiceModel;
using AccountRepository.DataAccess;
using Contracts;
using System;
using log4net;

namespace AccountRepository
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    class AccountRepository : IAccountRepository
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(AccountRepository));

        private IAccountDataAccess AccountDataAccess { get; set; }
        private IClientRepository ClientRepository { get; set; }

        public AccountRepository()
        {
            AccountDataAccess = ServiceLocator.Instance.Resolve<IAccountDataAccess>();
            //ClientRepository = ServiceLocator.Instance.Resolve<IServiceFactory>().GetClientRepository();
        }

        public bool ChangeAccountBalance(Guid accountId, long amount)
        {
            try
            {

                var account = GetAccountById(accountId);
                if (account == null)
                {
                    return false;
                }

                account.Money += amount;
                SaveOrUpdate(account);


                return true;
            }
            catch (Exception e)
            {
                log.Error("Fatal exception: cannot changed account balance", e);
                return false;
            }

        }

        public bool CreateAccount(Account details)
        {
            try
            {
                SaveOrUpdate(details);
                return true;
            }
            catch (Exception e)
            {
                log.Error("Fatal exception: cannot create account", e);
                return false;
            }

        }

        public Account GetAccountById(Guid accountId)
        {
            try
            {
               return AccountDataAccess.FindById(accountId);
            }
            catch (Exception e)
            {
                log.Error("Fatal exception: cannot get account by id", e);
                return null;
            }
        }

        public Account GetAccountInformation(string accountNumber)
        {
            try
            {
                return AccountDataAccess.FindByAccountNumber(accountNumber);
            }
            catch (Exception e)
            {
                log.Error("Fatal exception: cannot get account information", e);
                return null;
            }
        }

        private bool ClientExists(Guid clientId)
        {
            return ClientRepository.GetClientInformation(clientId) != null;
        }

        private void SaveOrUpdate(Account account)
        {
            AccountDataAccess.SaveOrUpdate(account);
            log.InfoFormat("Saved account details. Account id: {0}", account.Id);
        }
    }
}
