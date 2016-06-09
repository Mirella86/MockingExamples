using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Models;
using Models.Models;

namespace Models
{
    public class BankInitializer : DropCreateDatabaseIfModelChanges<BankContext>
    {
        protected override void Seed(BankContext context)
        {
            var clients = new List<Client>
            {
                new Client {Name="John Grisham", Age=45 },
                new Client {Name="Stephen King", Age=39 }
            };

            clients.ForEach(s => context.Clients.Add(s));
            context.SaveChanges();
            var accounts = new List<Account>
            {
                  new Account {  Id=1509, Name="Savings Account", Balance=150, ClientId=1},
                  new Account {Id=1510, Name="Current Account", Balance=13.5, ClientId=1 },
                  new Account { Id=2540, Name="Current Account", Balance=2.6, ClientId=2},
                  new Account {Id=2550, Name="Savings Account", Balance=1300, ClientId=2 }
            };
            accounts.ForEach(s => context.Accounts.Add(s));
            context.SaveChanges();

        }
    }
}