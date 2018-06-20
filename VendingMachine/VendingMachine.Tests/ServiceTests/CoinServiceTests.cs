using System.Web;
using NUnit.Framework;
using VendingMachine.Common.Classes;
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
        public void AcceptCoins_HavingZeroCurrentAndTotalCoins_GivenUnknown_AddUnknownToReturnCoinsAndReturnsInsertCoinMessage()
        {
            // Arrange
            var unknown = new Coin(.99M, .99M);

            // Act
            var result = _sut.AcceptCoins(unknown);

            // Assert
            Assert.AreEqual(decimal.Zero, SessionHelper.CurrentCoins.TotalValue());
            Assert.AreEqual(decimal.Zero, SessionHelper.TotalCoins.TotalValue());
            Assert.AreEqual(1, SessionHelper.ReturnCoins[Denominations.Unknown]);
            Assert.IsNotNull(result);
            Assert.AreEqual(MessageConstants.InsertCoin, result);
        }

        [Test]
        public void AcceptCoins_HavingZeroCurrentAndTotalCoins_GivenPenny_AddsPennyToReturnCoins()
        {
            // Arrange
            var penny = new Coin(WeightConstants.Penny, DiameterConstants.Penny);

            // Act
            var result = _sut.AcceptCoins(penny);

            // Assert
            Assert.AreEqual(decimal.Zero, SessionHelper.CurrentCoins.TotalValue());
            Assert.AreEqual(decimal.Zero, SessionHelper.TotalCoins.TotalValue());
            Assert.AreEqual(decimal.One, SessionHelper.ReturnCoins[Denominations.Penny]);
            Assert.AreEqual(Denominations.Penny.Value(), SessionHelper.ReturnCoins.TotalValue());
            Assert.IsNotNull(result);
            Assert.AreEqual(MessageConstants.InsertCoin, result);
        }

        [Test]
        public void AcceptCoins_HavingZeroCurrentAndTotalCoins_GivenNickel_AddsNickelToCurrentAndTotalCoins()
        {
            // Arrange
            var nickle = new Coin(WeightConstants.Nickle, DiameterConstants.Nickle);

            // Act
            var result = _sut.AcceptCoins(nickle);

            // Assert
            Assert.AreEqual(1, SessionHelper.CurrentCoins[Denominations.Nickel]);
            Assert.AreEqual(Denominations.Nickel.Value(), SessionHelper.CurrentCoins.TotalValue());
            Assert.AreEqual(1, SessionHelper.TotalCoins[Denominations.Nickel]);
            Assert.AreEqual(Denominations.Nickel.Value(), SessionHelper.TotalCoins.TotalValue());
            Assert.AreEqual(0, SessionHelper.ReturnCoins[Denominations.Nickel]);
            Assert.AreEqual(decimal.Zero, SessionHelper.ReturnCoins.TotalValue());
            Assert.IsNotNull(result);
            Assert.AreEqual($"{SessionHelper.CurrentCoins.TotalValue():C2}", result);
        }

        [Test]
        public void AcceptCoins_HavingZeroCurrentAndTotalCoins_GivenDime_AddsDimeToCurrentAndTotalCoins()
        {
            // Arrange
            var dime = new Coin(WeightConstants.Dime, DiameterConstants.Dime);

            // Act
            var result = _sut.AcceptCoins(dime);

            // Assert
            Assert.AreEqual(1, SessionHelper.CurrentCoins[Denominations.Dime]);
            Assert.AreEqual(Denominations.Dime.Value(), SessionHelper.CurrentCoins.TotalValue());
            Assert.AreEqual(1, SessionHelper.TotalCoins[Denominations.Dime]);
            Assert.AreEqual(Denominations.Dime.Value(), SessionHelper.TotalCoins.TotalValue());
            Assert.AreEqual(0, SessionHelper.ReturnCoins[Denominations.Dime]);
            Assert.AreEqual(decimal.Zero, SessionHelper.ReturnCoins.TotalValue());
            Assert.IsNotNull(result);
            Assert.AreEqual($"{SessionHelper.CurrentCoins.TotalValue():C2}", result);
        }

        [Test]
        public void AcceptCoins_HavingZeroCurrentAndTotalCoins_GivenQuarter_AddsQuarterToCurrentAndTotalCoins()
        {
            // Arrange
            var quarter = new Coin(WeightConstants.Quarter, DiameterConstants.Quarter);

            // Act
            var result = _sut.AcceptCoins(quarter);

            // Assert
            Assert.AreEqual(1, SessionHelper.CurrentCoins[Denominations.Quarter]);
            Assert.AreEqual(Denominations.Quarter.Value(), SessionHelper.CurrentCoins.TotalValue());
            Assert.AreEqual(1, SessionHelper.TotalCoins[Denominations.Quarter]);
            Assert.AreEqual(Denominations.Quarter.Value(), SessionHelper.TotalCoins.TotalValue());
            Assert.AreEqual(0, SessionHelper.ReturnCoins[Denominations.Quarter]);
            Assert.AreEqual(decimal.Zero, SessionHelper.ReturnCoins.TotalValue());
            Assert.IsNotNull(result);
            Assert.AreEqual($"{SessionHelper.CurrentCoins.TotalValue():C2}", result);
        }

        [Test]
        public void AcceptCoins_HavingZeroCurrentAndTotalCoins_GivenOneOfEachCoins_ReturnsCorrectCurrentTotalAndReturnCoins()
        {
            // Arrange
            var quarter = new Coin(WeightConstants.Quarter, DiameterConstants.Quarter);
            var dime = new Coin(WeightConstants.Dime, DiameterConstants.Dime);
            var nickle = new Coin(WeightConstants.Nickle, DiameterConstants.Nickle);
            var penny = new Coin(WeightConstants.Penny, DiameterConstants.Penny);
            var unknown = new Coin(.99M, .99M);
            const decimal totalCoins = .40M;

            // Act
            _sut.AcceptCoins(quarter);
            _sut.AcceptCoins(dime);
            _sut.AcceptCoins(nickle);
            _sut.AcceptCoins(penny);
            var result = _sut.AcceptCoins(unknown);

            // Assert
            Assert.AreEqual(1, SessionHelper.CurrentCoins[Denominations.Quarter]);
            Assert.AreEqual(1, SessionHelper.CurrentCoins[Denominations.Dime]);
            Assert.AreEqual(1, SessionHelper.CurrentCoins[Denominations.Nickel]);
            Assert.AreEqual(totalCoins, SessionHelper.CurrentCoins.TotalValue());
            Assert.AreEqual(1, SessionHelper.TotalCoins[Denominations.Quarter]);
            Assert.AreEqual(1, SessionHelper.TotalCoins[Denominations.Dime]);
            Assert.AreEqual(1, SessionHelper.TotalCoins[Denominations.Nickel]);
            Assert.AreEqual(totalCoins, SessionHelper.TotalCoins.TotalValue());
            Assert.AreEqual(1, SessionHelper.ReturnCoins[Denominations.Penny]);
            Assert.AreEqual(1, SessionHelper.ReturnCoins[Denominations.Unknown]);
            Assert.AreEqual(Denominations.Penny.Value(), SessionHelper.ReturnCoins.TotalValue());
            Assert.IsNotNull(result);
            Assert.AreEqual($"{SessionHelper.CurrentCoins.TotalValue():C2}", result);
        }

        [Test]
        public void ReturnCoins_HavingVariousCoins_ClearsCurrentCoinsAndReturnsSameCoins()
        {
            // Arrange
            var quarter = new Coin(WeightConstants.Quarter, DiameterConstants.Quarter);
            var dime = new Coin(WeightConstants.Dime, DiameterConstants.Dime);
            var nickle = new Coin(WeightConstants.Nickle, DiameterConstants.Nickle);
            var penny = new Coin(WeightConstants.Penny, DiameterConstants.Penny);
            var unknown = new Coin(.99M, .99M);
            const decimal totalCoins = .43M;

            // Act
            _sut.AcceptCoins(quarter);
            _sut.AcceptCoins(dime);
            _sut.AcceptCoins(nickle);
            _sut.AcceptCoins(penny);
            _sut.AcceptCoins(penny);
            _sut.AcceptCoins(penny);
            _sut.AcceptCoins(unknown);
            var result = _sut.ReturnCoins();

            // Assert
            Assert.AreEqual(decimal.Zero, SessionHelper.CurrentCoins.TotalValue());
            Assert.AreEqual(decimal.Zero, SessionHelper.TotalCoins.TotalValue());
            Assert.AreEqual(1, SessionHelper.ReturnCoins[Denominations.Quarter]);
            Assert.AreEqual(1, SessionHelper.ReturnCoins[Denominations.Dime]);
            Assert.AreEqual(1, SessionHelper.ReturnCoins[Denominations.Nickel]);
            Assert.AreEqual(3, SessionHelper.ReturnCoins[Denominations.Penny]);
            Assert.AreEqual(1, SessionHelper.ReturnCoins[Denominations.Unknown]);
            Assert.AreEqual(totalCoins, SessionHelper.ReturnCoins.TotalValue());
            Assert.IsNotNull(result);
            Assert.AreEqual(MessageConstants.InsertCoin, result);
        }

        #endregion
    }
}