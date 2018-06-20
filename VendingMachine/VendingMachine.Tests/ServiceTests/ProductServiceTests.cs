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
    public class ProductServiceTests
    {
        #region Fields

        private IProductService _sut;

        #endregion

        #region Setup/Teardown

        [SetUp]
        public void InitializeTests()
        {
            // HttpContext
            HttpContext.Current = MockHelper.FakeHttpContext();

            // SessionHelper
            SessionHelper.ClearAll();

            _sut = new ProductService();
        }

        #endregion

        #region Tests

        [Test]
        public void SelectProduct_HavingNoCoins_GivenCandy_ReturnsInsertCoin()
        {
            // Arrange
            const Products product = Products.Candy;

            // Act
            var result = _sut.SelectProduct(product);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(MessageConstants.InsertCoin, result);
        }

        [Test]
        public void SelectProduct_HavingNotEnoughCoins_GivenCandy_ReturnsCandyPrice()
        {
            // Arrange
            const Products product = Products.Candy;
            CoinHelper.AddCoin(Denominations.Quarter);
            CoinHelper.AddCoin(Denominations.Quarter);
            CoinHelper.AddCoin(Denominations.Dime);

            // Act
            var result = _sut.SelectProduct(product);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(string.Format(MessageConstants.Price, product.Price()), result);
        }

        [Test]
        public void SelectProduct_HavingExactCoins_GivenCandy_UpdatesInventoryClearsCurrentCoinsAndReturnsThankYou()
        {
            // Arrange
            const Products product = Products.Candy;
            CoinHelper.AddCoin(Denominations.Quarter);
            CoinHelper.AddCoin(Denominations.Quarter);
            CoinHelper.AddCoin(Denominations.Dime);
            CoinHelper.AddCoin(Denominations.Nickel);

            // Act
            var result = _sut.SelectProduct(product);

            // Assert
            Assert.AreEqual(2, SessionHelper.Inventory[Products.Candy]);
            Assert.AreEqual(3, SessionHelper.Inventory[Products.Chips]);
            Assert.AreEqual(3, SessionHelper.Inventory[Products.Cola]);
            Assert.AreEqual(decimal.Zero, SessionHelper.CurrentCoins.TotalValue());
            Assert.AreEqual(.65M, SessionHelper.TotalCoins.TotalValue());
            Assert.IsNotNull(result);
            Assert.AreEqual(MessageConstants.ThankYou, result);
        }

        [Test]
        public void SelectProduct_HavingMoreThanEnoughCoins_GivenCandy_ReturnsCoins()
        {
            // Arrange
            const Products product = Products.Candy;
            CoinHelper.AddCoin(Denominations.Quarter);
            CoinHelper.AddCoin(Denominations.Quarter);
            CoinHelper.AddCoin(Denominations.Quarter);
            CoinHelper.AddCoin(Denominations.Quarter);
            CoinHelper.AddCoin(Denominations.Dime);
            CoinHelper.AddCoin(Denominations.Dime);
            CoinHelper.AddCoin(Denominations.Dime);
            CoinHelper.AddCoin(Denominations.Dime);
            CoinHelper.AddCoin(Denominations.Nickel);
            CoinHelper.AddCoin(Denominations.Nickel);
            CoinHelper.AddCoin(Denominations.Nickel);
            CoinHelper.AddCoin(Denominations.Nickel);

            // Act
            var result = _sut.SelectProduct(product);

            // Assert
            Assert.AreEqual(3, SessionHelper.ReturnCoins[Denominations.Quarter]);
            Assert.AreEqual(2, SessionHelper.ReturnCoins[Denominations.Dime]);
            Assert.AreEqual(.95M, SessionHelper.ReturnCoins.TotalValue());
            Assert.IsNotNull(result);
            Assert.AreEqual(MessageConstants.ThankYou, result);
        }

        [Test]
        public void SelectProduct_HavingSoldOutCandy_GivenCandy_ReturnsCoins()
        {
            // Arrange
            const Products product = Products.Candy;
            SessionHelper.Inventory[product] = 0;
            CoinHelper.AddCoin(Denominations.Quarter);
            CoinHelper.AddCoin(Denominations.Quarter);
            CoinHelper.AddCoin(Denominations.Dime);
            CoinHelper.AddCoin(Denominations.Nickel);

            // Act
            var result = _sut.SelectProduct(product);

            // Assert
            Assert.AreEqual(2, SessionHelper.CurrentCoins[Denominations.Quarter]);
            Assert.AreEqual(1, SessionHelper.CurrentCoins[Denominations.Dime]);
            Assert.AreEqual(1, SessionHelper.CurrentCoins[Denominations.Nickel]);
            Assert.AreEqual(Products.Candy.Price(), SessionHelper.CurrentCoins.TotalValue());
            Assert.AreEqual(2, SessionHelper.TotalCoins[Denominations.Quarter]);
            Assert.AreEqual(1, SessionHelper.TotalCoins[Denominations.Dime]);
            Assert.AreEqual(1, SessionHelper.TotalCoins[Denominations.Nickel]);
            Assert.AreEqual(Products.Candy.Price(), SessionHelper.TotalCoins.TotalValue());
            Assert.AreEqual(decimal.Zero, SessionHelper.ReturnCoins.TotalValue());
            Assert.IsNotNull(result);
            Assert.AreEqual(MessageConstants.SoldOut, result);
        }

        [Test]
        public void SelectProduct_HavingNoExactChange_GivenCandy_ReturnsExactChangeMessage()
        {
            // Arrange
            const Products product = Products.Candy;
            CoinHelper.AddCoin(Denominations.Quarter);
            CoinHelper.AddCoin(Denominations.Quarter);
            CoinHelper.AddCoin(Denominations.Quarter);

            // Act
            var result = _sut.SelectProduct(product);

            // Assert
            Assert.AreEqual(3, SessionHelper.CurrentCoins[Denominations.Quarter]);
            Assert.AreEqual(.75M, SessionHelper.CurrentCoins.TotalValue());
            Assert.AreEqual(3, SessionHelper.TotalCoins[Denominations.Quarter]);
            Assert.AreEqual(.75M, SessionHelper.TotalCoins.TotalValue());
            Assert.AreEqual(decimal.Zero, SessionHelper.ReturnCoins.TotalValue());
            Assert.IsNotNull(result);
            Assert.AreEqual(MessageConstants.ExactChange, result);
        }

        #endregion
    }
}