using DAL.Interfaces;
using System.Data.Entity;
using Models.Models;

namespace DAL.Implementations
{
    public class AccountRepository : RepositoryBase<Account>, IAccountRepository
    {
        public AccountRepository(DbContext context) : base(context)
        {
        }
    }
}
