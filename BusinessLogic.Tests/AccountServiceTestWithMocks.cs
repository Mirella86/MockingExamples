using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using DAL.Interfaces;
using MockingExampleSolution.Implementations;
using MockingExampleSolution.Interfaces;
using Models.Models;
using Models.CustomExceptions;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BusinessLogic.Tests
{
    [TestClass]
    public class AccountServiceTestWithMocks
    {
        private Mock<IAccountRepository> _accountRepositoryMock;
        private Client _client;
        private IEnumerable<Account> _accounts;
        private IAccountService _accountServiceInTest;

        [TestInitialize]
        public void Initialize_Mocks()
        {
            _client = new Client { Id = 1, Age = 50, Name = "test user" };
            _accounts = new List<Account> {
                new Account {
                    Id=1, ClientId=1, Balance=0
                },
                 new Account {
                    Id=2, ClientId=1, Balance=50
                },
                  new Account {
                    Id=3, ClientId=1, Balance=500
                },
                  new Account
                  {
                      Id=4, ClientId=1, Balance=5000
                  },
                  new Account
                  {
                      Id=5, ClientId=1, Balance=10000
                  }
            };

            _accountRepositoryMock = new Mock<IAccountRepository>();
            _accountRepositoryMock.Setup(rep => rep.Find(It.IsAny<Expression<Func<Account, bool>>>()))
                    .Returns(new Func<Expression<Func<Account, bool>>, IQueryable<Account>>(
                    expr => _accounts.Where(expr.Compile()).AsQueryable()));

            _accountServiceInTest = new AccountService(_accountRepositoryMock.Object);
        }


        [TestMethod]
        [ExpectedException(typeof(AccountCustomException))]
        public void When_Account_Is_Not_Found_And_Try_To_Capitalize_Interest_Throw_Exception()
        {
            var actualResult = _accountServiceInTest.CapitalizeInterestForClientAccount(_client.Id, 1000);
        }

        [TestMethod]
        public void When_Balance_Is_0_Returns_0()
        {
            //Balance = 0
            var expectedResult = 0;
            var actualResult = _accountServiceInTest.CapitalizeInterestForClientAccount(1, 1);
            Assert.AreEqual(expectedResult, actualResult);

        }

        [TestMethod]
        public void When_Balance_Is_Under_100_Calculate_Interest_10_Percent()
        {
            //Balance = 50
            var expectedResult = 5;
            var actualResult = _accountServiceInTest.CapitalizeInterestForClientAccount(1, 2);
            Assert.AreEqual(expectedResult, actualResult);

        }

        [TestMethod]
        public void When_Balance_Is_Under_1000_Calculate_Interest_5_Percent()
        {
            //Balance = 500
            var expectedResult = 25;
            var actualResult = _accountServiceInTest.CapitalizeInterestForClientAccount(1, 3);
            Assert.AreEqual(expectedResult, actualResult);

        }

        [TestMethod]
        public void When_Balance_Is_Under_10000_Calculate_Interest_1_Percent()
        {
            //Balance = 5000
            double expectedResult = 50;
            double actualResult = _accountServiceInTest.CapitalizeInterestForClientAccount(1, 4);
            Assert.AreEqual(expectedResult, actualResult);

        }

        [TestMethod]
        public void When_Balance_Is_Over_10000_Return_0_Interest()
        {
            // Balance = 10 000
            double expectedResult = 0;
            double actualResult = _accountServiceInTest.CapitalizeInterestForClientAccount(1, 5);
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void When_Calculating_Interest_For_All_Account_Must_Increase_Current_Account_With_Interest()
        {
            var account = _accounts.Single(i => i.Id == 2);
            double expectedBalance = account.Balance + 5 + 25 + 50;

            _accountServiceInTest.AddInterestRateToAccountFromAllClientsAccounts(1, account);
            Assert.AreEqual(expectedBalance, account.Balance);
        }
    }
}
