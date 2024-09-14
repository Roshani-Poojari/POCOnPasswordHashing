using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;
using PasswordHashingThree.Models;

namespace PasswordHashingThree.Mappings
{
    public class AccountMap:ClassMap<Account>
    {
        public AccountMap()
        {
            Table("Accounts");
            Id(a => a.Id).GeneratedBy.Identity();
            Map(a => a.Username);
            Map(a => a.Password);
        }
    }
}