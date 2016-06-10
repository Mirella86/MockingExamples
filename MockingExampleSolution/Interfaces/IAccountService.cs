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
        void AddInterestRateToAccountFromAllClientsAccounts(int clientId, Account account);
    }
}
