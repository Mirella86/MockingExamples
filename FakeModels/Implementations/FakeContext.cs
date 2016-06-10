using Models.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Interfaces;
using Models;
using FakeModels.Implementations;

namespace FakeModels
{
    public class FakeContext : DbContext
    {
        public FakeContext()
        {
                    }
        public FakeDbSet<Client> Clients { get; set; }
        public FakeDbSet<Account> Accounts { get; set; }

    }
}
