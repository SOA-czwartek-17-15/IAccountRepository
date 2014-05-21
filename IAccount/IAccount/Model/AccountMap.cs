using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace AccountRepository.Model
{
    class AccountMap : ClassMap<AccountEntity>
    {
        public AccountMap()
        {
            Id(x => x.Id);
            Map(x => x.AccountId).Unique().Column("AccountId");
            Map(x => x.AccountNumber).Unique();
            Map(x => x.ClientId);
            Map(x => x.Money);
            Map(x => x.Percentage);
            Map(x => x.Type);
            Map(x => x.StartDate);
            Map(x => x.EndDate);
        }
    }
}
