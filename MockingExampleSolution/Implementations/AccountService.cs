using DAL.Interfaces;
using MockingExampleSolution.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Models;
using Models.CustomExceptions;

namespace MockingExampleSolution.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly double ThresholdMIN = 100;
        private readonly double ThresholdMID = 1000;
        private readonly double ThresholdMAX = 10000;

        protected IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepo)
        {
            _accountRepository = accountRepo;
        }

        public double CapitalizeInterestForClientAccount(int clientId, int accountId)
        {
            Account account = GetAccountByClientIdAndAccountId(clientId, accountId);
            if (account != null)
                return CalculateInterest(account.Balance);

            throw new AccountCustomException();
        }

        private Account GetAccountByClientIdAndAccountId(int clientId, int accountId)
            => _accountRepository.Find(i => i.Id == accountId && i.ClientId == clientId).SingleOrDefault();

        public double CalculateInterest(double balance)
        {
            if (balance < ThresholdMIN)
                return balance * 0.1;

            if (balance < ThresholdMID)
                return balance * 0.05;

            if (balance < ThresholdMAX)
                return balance * 0.01;

            return 0;
        }

        public void AddInterestRateToAccountFromAllClientsAccounts(int clientId, Account account)
        {
            //capitalize first current account
            account.Balance += CapitalizeInterestForClientAccount(clientId, account.Id);

            //capitalize for the rest of the accounts into the current
            IEnumerable<Account> allClientsAccounts = GetAllClientsAccounts(clientId).Where(i => i.Id != account.Id);
            foreach (var clientAccount in allClientsAccounts)
            {
                double calculatedInterest = CapitalizeInterestForClientAccount(clientId, clientAccount.Id);
                account.Balance += calculatedInterest;
            }
            _accountRepository.Update(account);
        }

        private IQueryable<Account> GetAllClientsAccounts(int clientId)
            => _accountRepository.Find(i => i.ClientId == clientId);
    }
}
