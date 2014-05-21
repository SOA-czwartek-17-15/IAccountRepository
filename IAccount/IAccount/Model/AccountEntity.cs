using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Contracts;

namespace AccountRepository.Model
{
    class AccountEntity
    {
        public AccountEntity()
        {

        }

        public AccountEntity(Account account)
        {
            this.Update(account);
        }

        public virtual void Update(Account account)
        {
            this.AccountId = account.Id;
            this.AccountNumber = account.AccountNumber;
            this.ClientId = account.ClientId;
            this.Money = account.Money;
            this.Type = account.Type;
            this.Percentage = account.Percentage;
            this.StartDate = account.StartDate;
            this.EndDate = account.EndDate;
        }

        public virtual Account GetAccount()
        {
            var account = new Account
            {
                Id = this.AccountId,
                AccountNumber = this.AccountNumber,
                ClientId = this.ClientId,
                Money = this.Money,
                Type = this.Type,
                Percentage = this.Percentage,
                StartDate = this.StartDate,
                EndDate = this.EndDate
            };
            return account;
        }
        public virtual int Id { get; set; }
        public virtual Guid AccountId { get; set; }
        public virtual string AccountNumber { get; set; }
        public virtual Guid ClientId { get; set; }
        public virtual long Money { get; set; }
        public virtual AccountType Type { get; set; }
        public virtual double Percentage { get; set; }
        public virtual DateTime EndDate { get; set; }
        public virtual DateTime StartDate { get; set; }
    }
}
