﻿using System.Web;
using NUnit.Framework;
using VendingMachine.Common.Constants;
using VendingMachine.Common.Enums;
using VendingMachine.Common.Extensions;
using VendingMachine.Common.Helpers;
using VendingMachine.Services;
using VendingMachine.Tests.Helpers;

namespace VendingMachine.Tests.ServiceTests
{
    [TestFixture]
    public class CoinServiceTests
    {
        #region Fields

        private ICoinService _sut;

        #endregion

        #region Setup/Teardown

        [SetUp]
        public void InitializeTests()
        {
            // HttpContext
            HttpContext.Current = MockHelper.FakeHttpContext();

            // SessionHelper
            SessionHelper.ClearAll();
            
            _sut = new CoinService();
        }

        #endregion

        #region Tests

        [Test]
        public void AcceptCoins_HavingZeroCurrentAndTotalCoins_GivenNothing_Returns_ZeroCurrentAndTotalCoins()
        {
            // Act
            var result = _sut.AcceptCoins(null);

            // Assert
            Assert.AreEqual(decimal.Zero, SessionHelper.CurrentCoins.TotalValue());
            Assert.AreEqual(decimal.Zero, SessionHelper.TotalCoins.TotalValue());
            Assert.IsNotNull(result);
        }

        [Test]
        public void AcceptCoins_HavingZeroCurrentAndTotalCoins_GivenNothing_ReturnsInsertCoinMessage()
        {
            // Act
            var result = _sut.AcceptCoins(null);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(MessageConstants.InsertCoin, result);
        }

        [Test]
        public void AcceptCoins_HavingZeroCurrentAndTotalCoins_GivenPenny_AddsPennyToReturnCoins()
        {
            // Arrange
            const Coins coin = Coins.Penny;

            // Act
            var result = _sut.AcceptCoins(coin);

            // Assert
            Assert.AreEqual(decimal.Zero, SessionHelper.CurrentCoins.TotalValue());
            Assert.AreEqual(decimal.Zero, SessionHelper.TotalCoins.TotalValue());
            Assert.AreEqual(decimal.One, SessionHelper.ReturnCoins[coin]);
            Assert.AreEqual(coin.Value(), SessionHelper.ReturnCoins.TotalValue());
            Assert.IsNotNull(result);
            Assert.AreEqual(MessageConstants.InsertCoin, result);
        }

        [Test]
        public void AcceptCoins_HavingZeroCurrentAndTotalCoins_GivenNickel_AddsNickelToCurrentAndTotalCoins()
        {
            // Arrange
            const Coins coin = Coins.Nickel;

            // Act
            var result = _sut.AcceptCoins(coin);

            // Assert
            Assert.AreEqual(decimal.One, SessionHelper.CurrentCoins[coin]);
            Assert.AreEqual(coin.Value(), SessionHelper.CurrentCoins.TotalValue());
            Assert.AreEqual(decimal.One, SessionHelper.TotalCoins[coin]);
            Assert.AreEqual(coin.Value(), SessionHelper.TotalCoins.TotalValue());
            Assert.AreEqual(decimal.Zero, SessionHelper.ReturnCoins[coin]);
            Assert.AreEqual(decimal.Zero, SessionHelper.ReturnCoins.TotalValue());
            Assert.IsNotNull(result);
            Assert.AreEqual($"{SessionHelper.CurrentCoins.TotalValue():C2}", result);
        }

        [Test]
        public void AcceptCoins_HavingZeroCurrentAndTotalCoins_GivenDime_AddsDimeToCurrentAndTotalCoins()
        {
            // Arrange
            const Coins coin = Coins.Dime;

            // Act
            var result = _sut.AcceptCoins(coin);

            // Assert
            Assert.AreEqual(decimal.One, SessionHelper.CurrentCoins[coin]);
            Assert.AreEqual(coin.Value(), SessionHelper.CurrentCoins.TotalValue());
            Assert.AreEqual(decimal.One, SessionHelper.TotalCoins[coin]);
            Assert.AreEqual(coin.Value(), SessionHelper.TotalCoins.TotalValue());
            Assert.AreEqual(decimal.Zero, SessionHelper.ReturnCoins[coin]);
            Assert.AreEqual(decimal.Zero, SessionHelper.ReturnCoins.TotalValue());
            Assert.IsNotNull(result);
            Assert.AreEqual($"{SessionHelper.CurrentCoins.TotalValue():C2}", result);
        }

        [Test]
        public void AcceptCoins_HavingZeroCurrentAndTotalCoins_GivenQuarter_AddsQuarterToCurrentAndTotalCoins()
        {
            // Arrange
            const Coins coin = Coins.Quarter;

            // Act
            var result = _sut.AcceptCoins(coin);

            // Assert
            Assert.AreEqual(decimal.One, SessionHelper.CurrentCoins[coin]);
            Assert.AreEqual(coin.Value(), SessionHelper.CurrentCoins.TotalValue());
            Assert.AreEqual(decimal.One, SessionHelper.TotalCoins[coin]);
            Assert.AreEqual(coin.Value(), SessionHelper.TotalCoins.TotalValue());
            Assert.AreEqual(decimal.Zero, SessionHelper.ReturnCoins[coin]);
            Assert.AreEqual(decimal.Zero, SessionHelper.ReturnCoins.TotalValue());
            Assert.IsNotNull(result);
            Assert.AreEqual($"{SessionHelper.CurrentCoins.TotalValue():C2}", result);
        }

        [Test]
        public void AcceptCoins_HavingZeroCurrentAndTotalCoins_GivenOneOfEachCoins_ReturnsCorrectCurrentTotalAndReturnCoins()
        {
            // Arrange
            const decimal totalCoins = .40M;

            // Act
            _sut.AcceptCoins(Coins.Quarter);
            _sut.AcceptCoins(Coins.Dime);
            _sut.AcceptCoins(Coins.Nickel);
            var result = _sut.AcceptCoins(Coins.Penny);

            // Assert
            Assert.AreEqual(decimal.One, SessionHelper.CurrentCoins[Coins.Quarter]);
            Assert.AreEqual(decimal.One, SessionHelper.CurrentCoins[Coins.Dime]);
            Assert.AreEqual(decimal.One, SessionHelper.CurrentCoins[Coins.Nickel]);
            Assert.AreEqual(totalCoins, SessionHelper.CurrentCoins.TotalValue());
            Assert.AreEqual(decimal.One, SessionHelper.TotalCoins[Coins.Quarter]);
            Assert.AreEqual(decimal.One, SessionHelper.TotalCoins[Coins.Dime]);
            Assert.AreEqual(decimal.One, SessionHelper.TotalCoins[Coins.Nickel]);
            Assert.AreEqual(totalCoins, SessionHelper.TotalCoins.TotalValue());
            Assert.AreEqual(decimal.One, SessionHelper.ReturnCoins[Coins.Penny]);
            Assert.AreEqual(Coins.Penny.Value(), SessionHelper.ReturnCoins.TotalValue());
            Assert.IsNotNull(result);
            Assert.AreEqual($"{SessionHelper.CurrentCoins.TotalValue():C2}", result);
        }

        [Test]
        public void ReturnCoins_HavingVariousCoins_ClearsCurrentCoinsAndReturnsSameCoins()
        {
            // Arrange
            const decimal totalCoins = .43M;

            // Act
            _sut.AcceptCoins(Coins.Quarter);
            _sut.AcceptCoins(Coins.Dime);
            _sut.AcceptCoins(Coins.Nickel);
            _sut.AcceptCoins(Coins.Penny);
            _sut.AcceptCoins(Coins.Penny);
            _sut.AcceptCoins(Coins.Penny);
            var result = _sut.ReturnCoins();

            // Assert
            Assert.AreEqual(decimal.Zero, SessionHelper.CurrentCoins.TotalValue());
            Assert.AreEqual(1, SessionHelper.ReturnCoins[Coins.Quarter]);
            Assert.AreEqual(1, SessionHelper.ReturnCoins[Coins.Dime]);
            Assert.AreEqual(1, SessionHelper.ReturnCoins[Coins.Nickel]);
            Assert.AreEqual(3, SessionHelper.ReturnCoins[Coins.Penny]);
            Assert.AreEqual(totalCoins, SessionHelper.ReturnCoins.TotalValue());
            Assert.IsNotNull(result);
            Assert.AreEqual(MessageConstants.InsertCoin, result);
        }

        #endregion
    }
}