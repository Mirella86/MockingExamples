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

        public virtual double CapitalizeInterestForClientAccount(int clientId, int accountId)
        {
            var account = FindAccountByClientIdAndAccountId(clientId, accountId);
            if (account != null)
                return CalculateInterest(account.Balance);

            throw new AccountCustomException();
        }

        public virtual double CalculateInterest(double balance)
        {
            if (balance < ThresholdMIN)
                return balance * 0.1;

            if (balance < ThresholdMID)
                return balance * 0.05;

            if (balance < ThresholdMAX)
                return balance * 0.01;

            return 0;
        }

        public virtual Account FindAccountByClientIdAndAccountId(int clientId, int accountId)
            => _accountRepository.Get(i => i.ClientId == clientId && i.Id == accountId).SingleOrDefault();

        public virtual Account FindClientCurrentAccount(int clientId)
            => _accountRepository.Get(i => i.ClientId == clientId && i.Name.ToLower().Contains("current")).FirstOrDefault();

        public virtual void AddInterestRateToAccount(int clientId, Account account)
        {
            var allClientsAccounts = GetAllClientAcounts(clientId);
            var calculatedInterest = CapitalizeInterestForClientAccount(clientId, accountId);
            account.Balance += calculatedInterest;
        }

        public virtual IEnumerable<Account> GetAllClientAcounts(int clientId)
        => _accountRepository.Get(i => i.ClientId == clientId);
    }
}
