using System.Web;
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
            _sut.AcceptCoins(null);

            // Assert
            Assert.AreEqual(decimal.Zero, SessionHelper.CurrentCoins.ToTotalValue());
            Assert.AreEqual(decimal.Zero, SessionHelper.TotalCoins.ToTotalValue());
        }

        [Test]
        public void AcceptCoins_HavingZeroCurrentAndTotalCoins_GivenNothing_ReturnsInsertCoinMessage()
        {
            // Act
            _sut.AcceptCoins(null);

            // Assert
            Assert.AreEqual(SessionConstants.InsertCoin, SessionHelper.Message);
        }

        [Test]
        public void AcceptCoins_HavingZeroCurrentAndTotalCoins_GivenPenny_AddsPennyToReturnCoins()
        {
            // Arrange
            var coin = Coins.Penny;

            // Act
            _sut.AcceptCoins(coin);

            // Assert
            Assert.AreEqual(decimal.Zero, SessionHelper.CurrentCoins.ToTotalValue());
            Assert.AreEqual(decimal.Zero, SessionHelper.TotalCoins.ToTotalValue());
            Assert.AreEqual(decimal.One, SessionHelper.ReturnCoins[coin]);
            Assert.AreEqual(coin.ToValue(), SessionHelper.ReturnCoins.ToTotalValue());
            Assert.AreEqual(SessionConstants.InsertCoin, SessionHelper.Message);
        }

        [Test]
        public void AcceptCoins_HavingZeroCurrentAndTotalCoins_GivenNickel_AddsNickelToCurrentAndTotalCoins()
        {
            // Arrange
            var coin = Coins.Nickel;

            // Act
            _sut.AcceptCoins(coin);

            // Assert
            Assert.AreEqual(decimal.One, SessionHelper.CurrentCoins[coin]);
            Assert.AreEqual(coin.ToValue(), SessionHelper.CurrentCoins.ToTotalValue());
            Assert.AreEqual(decimal.One, SessionHelper.TotalCoins[coin]);
            Assert.AreEqual(coin.ToValue(), SessionHelper.TotalCoins.ToTotalValue());
            Assert.AreEqual(decimal.Zero, SessionHelper.ReturnCoins[coin]);
            Assert.AreEqual(decimal.Zero, SessionHelper.ReturnCoins.ToTotalValue());
            Assert.AreEqual(string.Empty, SessionHelper.Message);
        }

        [Test]
        public void AcceptCoins_HavingZeroCurrentAndTotalCoins_GivenDime_AddsDimeToCurrentAndTotalCoins()
        {
            // Arrange
            var coin = Coins.Dime;

            // Act
            _sut.AcceptCoins(coin);

            // Assert
            Assert.AreEqual(decimal.One, SessionHelper.CurrentCoins[coin]);
            Assert.AreEqual(coin.ToValue(), SessionHelper.CurrentCoins.ToTotalValue());
            Assert.AreEqual(decimal.One, SessionHelper.TotalCoins[coin]);
            Assert.AreEqual(coin.ToValue(), SessionHelper.TotalCoins.ToTotalValue());
            Assert.AreEqual(decimal.Zero, SessionHelper.ReturnCoins[coin]);
            Assert.AreEqual(decimal.Zero, SessionHelper.ReturnCoins.ToTotalValue());
            Assert.AreEqual(string.Empty, SessionHelper.Message);
        }

        [Test]
        public void AcceptCoins_HavingZeroCurrentAndTotalCoins_GivenQuarter_AddsQuarterToCurrentAndTotalCoins()
        {
            // Arrange
            var coin = Coins.Quarter;

            // Act
            _sut.AcceptCoins(coin);

            // Assert
            Assert.AreEqual(decimal.One, SessionHelper.CurrentCoins[coin]);
            Assert.AreEqual(coin.ToValue(), SessionHelper.CurrentCoins.ToTotalValue());
            Assert.AreEqual(decimal.One, SessionHelper.TotalCoins[coin]);
            Assert.AreEqual(coin.ToValue(), SessionHelper.TotalCoins.ToTotalValue());
            Assert.AreEqual(decimal.Zero, SessionHelper.ReturnCoins[coin]);
            Assert.AreEqual(decimal.Zero, SessionHelper.ReturnCoins.ToTotalValue());
            Assert.AreEqual(string.Empty, SessionHelper.Message);
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
            _sut.AcceptCoins(Coins.Penny);

            // Assert
            Assert.AreEqual(decimal.One, SessionHelper.CurrentCoins[Coins.Quarter]);
            Assert.AreEqual(decimal.One, SessionHelper.CurrentCoins[Coins.Dime]);
            Assert.AreEqual(decimal.One, SessionHelper.CurrentCoins[Coins.Nickel]);
            Assert.AreEqual(totalCoins, SessionHelper.CurrentCoins.ToTotalValue());
            Assert.AreEqual(decimal.One, SessionHelper.TotalCoins[Coins.Quarter]);
            Assert.AreEqual(decimal.One, SessionHelper.TotalCoins[Coins.Dime]);
            Assert.AreEqual(decimal.One, SessionHelper.TotalCoins[Coins.Nickel]);
            Assert.AreEqual(totalCoins, SessionHelper.TotalCoins.ToTotalValue());
            Assert.AreEqual(decimal.One, SessionHelper.ReturnCoins[Coins.Penny]);
            Assert.AreEqual(Coins.Penny.ToValue(), SessionHelper.ReturnCoins.ToTotalValue());
            Assert.AreEqual(string.Empty, SessionHelper.Message);
        }

        #endregion
    }
}