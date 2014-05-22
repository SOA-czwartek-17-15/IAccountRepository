using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AccountRepository.DataAccess;
using Castle.MicroKernel.Registration;
using Contracts;
using NUnit.Framework;

namespace AccountRepository.Tests
{
    [TestFixture]
    class AccountTest
    {
        [Test]
        public void TestCreatingAccount()
        {
            var detail = CreateNewAccount();

            AccountRepository accountRepository = new AccountRepository();
            accountRepository.CreateAccount(detail);

            var  retrivedAccount = accountRepository.GetAccountById(detail.Id);

            Assert.IsNotNull(retrivedAccount);
            Assert.AreEqual(retrivedAccount.Money, detail.Money);
        }

        [Test]
        public void TestAccountBalance()
        {
            var detail = CreateNewAccount();
            detail.AccountNumber = "MyNumber";
            var currentMoney = detail.Money;
            AccountRepository accountRepository = new AccountRepository();
            accountRepository.CreateAccount(detail);

            bool changedBalance = accountRepository.ChangeAccountBalance(detail.Id, 100);
            Assert.IsTrue(changedBalance);


            var retrivedAccount = accountRepository.GetAccountInformation(detail.AccountNumber);
            
            Assert.IsNotNull(retrivedAccount);
            Assert.AreEqual(retrivedAccount.Money, currentMoney+100);
        }

        [TestFixtureSetUp]
        public void SetUp()
        {
            ServiceLocator.Instance.Register(Component.For<IServiceFactory>().Instance(new MockServiceFactory()).IsDefault().Named(Guid.NewGuid().ToString()));
            //ServiceLocator.Instance.Register(Component.For<IAccountDataAccess>().Instance( new MockAccountDataAccess()).IsDefault().Named(Guid.NewGuid().ToString()));
        }

        private static Account CreateNewAccount()
        {
            Account detail = new Account();
            detail.Id = Guid.NewGuid();
            detail.Money = 1000;
            detail.AccountNumber = "1";
            return detail;
        }
    }
}
