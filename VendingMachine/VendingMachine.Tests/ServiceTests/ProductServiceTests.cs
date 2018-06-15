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
            SessionHelper.AddCoin(Coins.Quarter);
            SessionHelper.AddCoin(Coins.Quarter);
            SessionHelper.AddCoin(Coins.Dime);

            // Act
            var result = _sut.SelectProduct(product);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(string.Format(MessageConstants.Price, product.ToValue()), result);
        }

        [Test]
        public void SelectProduct_HavingExactCoins_GivenCandy_UpdatesInventoryClearsCurrentCoinsAndReturnsThankYou()
        {
            // Arrange
            const Products product = Products.Candy;
            SessionHelper.AddCoin(Coins.Quarter);
            SessionHelper.AddCoin(Coins.Quarter);
            SessionHelper.AddCoin(Coins.Dime);
            SessionHelper.AddCoin(Coins.Nickel);

            // Act
            var result = _sut.SelectProduct(product);

            // Assert
            Assert.AreEqual(2, SessionHelper.Inventory[Products.Candy]);
            Assert.AreEqual(3, SessionHelper.Inventory[Products.Chips]);
            Assert.AreEqual(3, SessionHelper.Inventory[Products.Cola]);
            Assert.AreEqual(decimal.Zero, SessionHelper.CurrentCoins.ToTotalValue());
            Assert.AreEqual(.65M, SessionHelper.TotalCoins.ToTotalValue());
            Assert.IsNotNull(result);
            Assert.AreEqual(MessageConstants.ThankYou, result);
        }

        #endregion
    }
}