using DAL.Interfaces;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace FakeModels.Implementations
{
    public class FakeClientRepository : FakeRepositoryBase<Client>, IClientRepository
    {
        public FakeClientRepository(DbContext context) : base(context)
        {
        }

        public IEnumerable<Client> GetAllClients() => Context.Clients.ToList();

    }
}
