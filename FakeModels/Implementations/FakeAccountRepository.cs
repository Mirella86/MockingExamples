using DAL.Interfaces;
using Models.Models;
using System.Data.Entity;

namespace FakeModels.Implementations
{
    public class FakeAccountRepository : FakeRepositoryBase<Account>, IAccountRepository
    {
        public FakeAccountRepository(DbContext context) : base(context)
        {
        }

    }
}
