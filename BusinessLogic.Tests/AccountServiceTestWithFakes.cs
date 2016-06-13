using FakeModels;
using FakeModels.Implementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MockingExampleSolution.Implementations;
using MockingExampleSolution.Interfaces;
using Models.CustomExceptions;
using Models.Models;
using System.Collections.Generic;
using System.Data.Entity;

namespace BusinessLogic.Tests
{
    [TestClass]
    [Ignore]
    public class AccountServiceTestWithFakes
    {
        FakeAccountRepository _fakeAccountRepository;
        FakeContext _fakeDbContext;
        private IEnumerable<Account> _accounts;
        private IAccountService _accountServiceInTest;

        [TestInitialize]
        public void InitializeTestData()
        {
            _fakeDbContext = new FakeContext();
            _fakeAccountRepository = new FakeAccountRepository(_fakeDbContext);

            _fakeDbContext.Clients = new FakeDbSet<Client>();
            _fakeDbContext.Clients.Add(new Client { Id = 1, Age = 50, Name = "test user" });

            _fakeDbContext.Accounts = new FakeDbSet<Account>();
            _fakeDbContext.Accounts.Add(new Account
            {
                Id = 1,
                ClientId = 1,
                Balance = 0
            });
            _fakeDbContext.Accounts.Add(new Account
            {
                Id = 2,
                ClientId = 1,
                Balance = 50
            });
            _fakeDbContext.Accounts.Add(new Account
            {
                Id = 3,
                ClientId = 1,
                Balance = 500
            });
            _fakeDbContext.Accounts.Add(new Account
            {
                Id = 4,
                ClientId = 1,
                Balance = 5000
            });
            _fakeDbContext.Accounts.Add(new Account
            {
                Id = 5,
                ClientId = 1,
                Balance = 10000
            });


            _accountServiceInTest = new AccountService(_fakeAccountRepository);

        }


        [TestMethod]
        [ExpectedException(typeof(AccountCustomException))]
        public void When_Account_Is_Not_Found_And_Try_To_Capitalize_Interest_Throw_Exception()
        {
            var actualResult = _accountServiceInTest.CapitalizeInterestForClientAccount(1, 1000);
        }

        [TestMethod]
        public void When_Balance_Is_0_Returns_0()
        {
            //Balance = 0
            var expectedResult = 0;
            var actualResult = _accountServiceInTest.CapitalizeInterestForClientAccount(1, 1);
            Assert.AreEqual(expectedResult, actualResult);

        }
    }
}
