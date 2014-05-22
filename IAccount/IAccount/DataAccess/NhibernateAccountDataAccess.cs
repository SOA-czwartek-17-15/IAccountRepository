using System;
using AccountRepository.Model;
using Contracts;
using NHibernate;
using NHibernate.Criterion;

namespace AccountRepository.DataAccess
{
    class NhibernateAccountDataAccess : IAccountDataAccess
    {
        private ISessionFactory sessionFactory;
        public NhibernateAccountDataAccess()
        {
            sessionFactory = SessionFactory.CreateSessionFactory();
        }

        public void SaveOrUpdate(Account account)
        {
            using (var session = sessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {

                   var accountEntity = GetAccountEntityById(account.Id, session);
                    if (accountEntity == null)
                    {
                        accountEntity = new AccountEntity(account);
                    }
                    else
                    {
                        accountEntity.Update(account);
                    }
                    session.SaveOrUpdate(accountEntity);
                    transaction.Commit();
                }   

            }
        }

        public Account FindById(Guid accountId)
        {
            using (var session = sessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var accountEntity = GetAccountEntityById(accountId, session);
                    return accountEntity.GetAccount();
                }
            }
        }

        private static AccountEntity GetAccountEntityById(Guid accountId, ISession session)
        {
            var c = session.CreateCriteria<AccountEntity>();
            c.Add(Expression.Eq("AccountId", accountId));
            var accountEntity = c.UniqueResult<AccountEntity>();
            return accountEntity;
        }

        public Account FindByAccountNumber(string accountNumber)
        {
            using (var session = sessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var c = session.CreateCriteria<AccountEntity>();
                    c.Add(Expression.Eq("AccountNumber", accountNumber));
                    var accountEntity = c.UniqueResult<AccountEntity>();
                    return accountEntity.GetAccount();
                }
            }
        }
    }
}