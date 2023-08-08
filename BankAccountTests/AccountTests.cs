using Microsoft.VisualStudio.TestTools.UnitTesting;
using BankAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount.Tests
{
    [TestClass()]
    public class AccountTests
    {
        private Account acc;

        [TestInitialize]
        public void CreateDefaultAccount()
        {
            acc = new Account("J Doe");
        }

        [TestMethod()]
        [DataRow(100)]
        [DataRow(.01)]
        [DataRow(1.999)]
        [DataRow(9_999.99)]
        public void Deposit_APositiveAmount_AddToBalance(double depositAmount)
        {
            acc.Deposit(depositAmount);

            Assert.AreEqual(depositAmount, acc.Balance);
        }

        [TestMethod]
        public void Deposit_APostitiveAmount_ReturnsUpdatedBalance()
        {
            // AAA - Arrange Act Assert
            // Arrange
            double depositAmount = 100;
            double expectedReturn = 100;

            // Act
            double returnValue = acc.Deposit(depositAmount);

            // Assert
            Assert.AreEqual(expectedReturn, returnValue);
        }

        [TestMethod]
        [DataRow(-1)] // runs test with difference values
        [DataRow(0)]
        public void Deposit_ZeroOrLess_ThrowsArgumentException(double invalidDepositAmount)
        {
            // Arrange
            // Nothing to add

            // Assert => ACT
            Assert.ThrowsException<ArgumentOutOfRangeException>
                (() => acc.Deposit(invalidDepositAmount));
        }

        [TestMethod]
        [DataRow(100, 50)]
        [DataRow(100, .99)]
        public void Withdraw_PositiveAmount_ReturnsUpdatedBalance(double initialDeposit, double withdrawlAmount)
        {
            // Arrange
            double expectedBalance = initialDeposit - withdrawlAmount;
            acc.Deposit(initialDeposit);

            // Act
            double returnedBalance = acc.Withdraw(withdrawlAmount);

            // Assert
            Assert.AreEqual(expectedBalance, returnedBalance);
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(-.01)]
        [DataRow(-1000)]
        public void Withdraw_ZeroOrLess_ThrowsArgumentOutOfRangeException(double withdrawlAmount)
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => acc.Withdraw(withdrawlAmount));
        }

        [TestMethod]
        public void Withdraw_MoreThanAvailableBalance_ThrowsArgumentException()
        {
            double withdrawAmount = 1000;

            Assert.ThrowsException<ArgumentException>(() => acc.Withdraw(withdrawAmount));
        }


        [TestMethod]
        public void Withdraw_PositiveAmount_DecreasesBalance()
        {
            // Arrange
            double initialDeposit = 100;
            double withdrawlAmount = 50;
            double expectedBalance = initialDeposit - withdrawlAmount;

            // Act
            acc.Deposit(initialDeposit);
            acc.Withdraw(withdrawlAmount);

            double actualBalance = acc.Balance;

            // Assert
            Assert.AreEqual(withdrawlAmount, actualBalance);
        }
    }
}

// Withdrawing a positive amount - returns updated balance
// Withdrawing 0 - Throws ArgumentOutRange exception
// Withdrawing negative amount - Throws ArgumentOutRange exception
// Withdrawing more than balance - ArgumentException
