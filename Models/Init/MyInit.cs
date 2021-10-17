using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebAPIBank.Models.Context;
using WebAPIBank.Models.Entities;

namespace WebAPIBank.Models.Init
{
    public class MyInit:CreateDatabaseIfNotExists<MyContext>
    {
        protected override void Seed(MyContext context)
        {
            CustomerCardInfo cif = new CustomerCardInfo
            {
                CardUserName = "Selim Demirer",
                CardNumber = "1111 2222 3333 4444",
                CardExpiryYear = 2022,
                CardExpiryMonth = 12,
                SecurityNumber = "222",
                Limit = 15000,
                Balance = 10000
            };
            context.CustomerCardInfos.Add(cif);
            context.SaveChanges();
        }

    }
}