using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using DAL.Interfaces;
using MockingExampleSolution.Implementations;
using MockingExampleSolution.Interfaces;
using Models.Models;
using Models.CustomExceptions;

namespace BusinessLogic.Tests
{
    [TestClass]
    public class AccountServiceTest
    {
        private Mock<IAccountRepository> _accountRepositoryMock;

        [TestInitialize]
        public void Initialize_Mocks()
        {
            _accountRepositoryMock = new Mock<IAccountRepository>();

        }


        [TestMethod]
        [ExpectedException(typeof(AccountCustomException))]
        public void When_Account_Is_Not_Found_Throw_Exception()
        {
            var accountServiceMock = new Mock<AccountService>(_accountRepositoryMock.Object) { CallBase = true };
            accountServiceMock.Setup(x => x.FindAccountByClientIdAndAccountId(0, 0)).Returns((Account)null);

            var actualResult = accountServiceMock.Object.CapitalizeInterestForClientAccount(0, 0);

        }

        [TestMethod]
        public void When_Balance_Is_0_Returns_0()
        {
            var accountServiceMock = new Mock<AccountService>(_accountRepositoryMock.Object) { CallBase = true };
            accountServiceMock.Setup(x => x.FindAccountByClientIdAndAccountId(0, 0)).Returns(new Account
            {
                Balance = 0
            });

            var expectedResult = 0;
            var actualResult = accountServiceMock.Object.CapitalizeInterestForClientAccount(0, 0);
            Assert.AreEqual(expectedResult, actualResult);

        }

        [TestMethod]
        public void When_Balance_Is_Under_100_Calculate_Interest_10_Percent()
        {
            var accountServiceMock = new Mock<AccountService>(_accountRepositoryMock.Object) { CallBase = true };
            accountServiceMock.Setup(x => x.FindAccountByClientIdAndAccountId(It.IsAny<int>(), It.IsAny<int>())).Returns(new Account
            {
                Balance = 50
            });

            var expectedResult = 5;
            var actualResult = accountServiceMock.Object.CapitalizeInterestForClientAccount(0, 0);
            Assert.AreEqual(expectedResult, actualResult);

        }

        [TestMethod]
        public void When_Balance_Is_Under_1000_Calculate_Interest_5_Percent()
        {
            var accountServiceMock = new Mock<AccountService>(_accountRepositoryMock.Object) { CallBase = true };
            accountServiceMock.Setup(x => x.FindAccountByClientIdAndAccountId(It.IsAny<int>(), It.IsAny<int>())).Returns(new Account
            {
                Balance = 500
            });

            var expectedResult = 25;
            var actualResult = accountServiceMock.Object.CapitalizeInterestForClientAccount(0, 0);
            Assert.AreEqual(expectedResult, actualResult);

        }

        [TestMethod]
        public void When_Balance_Is_Under_10000_Calculate_Interest_1_Percent()
        {
            var accountServiceMock = new Mock<AccountService>(_accountRepositoryMock.Object) { CallBase = true };
            accountServiceMock.Setup(x => x.FindAccountByClientIdAndAccountId(It.IsAny<int>(), It.IsAny<int>())).Returns(new Account
            {
                Balance = 5000
            });

            double expectedResult = 50;
            double actualResult = accountServiceMock.Object.CapitalizeInterestForClientAccount(0, 0);
            Assert.AreEqual(expectedResult, actualResult);

        }

        [TestMethod]
        public void When_Balance_Is_Over_10000_Return_0_Interest()
        {
            var accountServiceMock = new Mock<AccountService>(_accountRepositoryMock.Object) { CallBase = true };
            accountServiceMock.Setup(x => x.FindAccountByClientIdAndAccountId(It.Is<int>(s => s > 0), It.Is<int>(s => s > 0))).Returns(new Account { Balance = 10000 });

            double expectedResult = 0;
            double actualResult = accountServiceMock.Object.CapitalizeInterestForClientAccount(10, 10);
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void When_Calculating_Interest_Must_Increase_Current_Account_With_Interest()
        {
            var accountServiceMock = new Mock<AccountService>(_accountRepositoryMock.Object) { CallBase = true };
            accountServiceMock.Setup(x => x.FindAccountByClientIdAndAccountId(It.Is<int>(s => s > 0), It.Is<int>(s => s > 0))).Returns(new Account { Balance = 10000 });
            accountServiceMock.Setup(x => x.FindClientCurrentAccount(It.IsAny<int>())).Returns(new Account { Balance = 100 });

        }
    }
}
