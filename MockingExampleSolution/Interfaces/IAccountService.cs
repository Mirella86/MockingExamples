using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockingExampleSolution.Interfaces
{
    public interface IAccountService : IService
    {
        double CapitalizeInterestForClientAccount(int clientId, int accountId);
        double CalculateInterest(double balance);
        Account FindAccountByClientIdAndAccountId(int clientId, int accountId);
        Account FindClientCurrentAccount(int ClientId);
    }
}
