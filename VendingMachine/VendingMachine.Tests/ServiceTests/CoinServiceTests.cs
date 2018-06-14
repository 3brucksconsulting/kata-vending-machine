using NUnit.Framework;
using System.Web;
using VendingMachine.Common;
using VendingMachine.Common.Enums;
using VendingMachine.Common.Extensions;
using VendingMachine.Services;
using VendingMachine.Tests.Helpers;

namespace VendingMachine.Tests
{
    [TestFixture]
    public class CoinServiceTests
    {
        #region Fields

        private CoinBox _coinBox;
        private ICoinService _sut;

        #endregion

        #region Setup/Teardown

        [SetUp]
        public void InitializeTests()
        {
            // HttpContext
            HttpContext.Current = MockHelper.FakeHttpContext();

            // CoinBox
            _coinBox = new CoinBox();
            _coinBox.Clear();

            _sut = new CoinService();
        }

        #endregion

        #region Tests

        [Test]
        public void AcceptCoins_HavingZeroCurrentAndTotalCoins_GivenNothing_Returns_ZeroCurrentAndTotalCoins()
        {
            // Arrange
            Coins? coin = null;

            // Act
            var result = _sut.AcceptCoins(coin);

            // Assert
            Assert.IsInstanceOf<CoinBox>(result);
            Assert.AreEqual(0D, result.CurrentCoins);
            Assert.AreEqual(0D, result.TotalCoins);
        }

        [Test]
        public void AcceptCoins_HavingZeroCurrentAndTotalCoins_GivenNothing_ReturnsInsertCoinMessage()
        {
            // Arrange
            Coins? coin = null;

            // Act
            var result = _sut.AcceptCoins(coin);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(CoinBox.Constants.InsertCoin, result.Message);
        }

        [Test]
        public void AcceptCoins_HavingZeroCurrentAndTotalCoins_GivenPenny_AddsPennyToReturnCoins()
        {
            // Arrange
            var coin = Coins.Penny;

            // Act
            var result = _sut.AcceptCoins(coin);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0D, result.CurrentCoins);
            Assert.AreEqual(0D, result.TotalCoins);
            Assert.AreEqual(coin.ToValue(), result.ReturnCoins);
            Assert.AreEqual(CoinBox.Constants.InsertCoin, result.Message);
        }

        [Test]
        public void AcceptCoins_HavingZeroCurrentAndTotalCoins_GivenNickel_AddsNickelToCurrentAndTotalCoins()
        {
            // Arrange
            var coin = Coins.Nickel;

            // Act
            var result = _sut.AcceptCoins(coin);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(coin.ToValue(), result.CurrentCoins);
            Assert.AreEqual(coin.ToValue(), result.TotalCoins);
            Assert.AreEqual(0D, result.ReturnCoins);
            Assert.AreEqual(string.Empty, result.Message);
        }

        #endregion
    }
}