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
            CoinHelper.AddCoin(Coins.Quarter);
            CoinHelper.AddCoin(Coins.Quarter);
            CoinHelper.AddCoin(Coins.Dime);

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
            CoinHelper.AddCoin(Coins.Quarter);
            CoinHelper.AddCoin(Coins.Quarter);
            CoinHelper.AddCoin(Coins.Dime);
            CoinHelper.AddCoin(Coins.Nickel);

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

        [Test]
        public void SelectProduct_HavingMoreThanEnoughCoins_GivenCandy_ReturnsCoins()
        {
            // Arrange
            const Products product = Products.Candy;
            CoinHelper.AddCoin(Coins.Quarter);
            CoinHelper.AddCoin(Coins.Quarter);
            CoinHelper.AddCoin(Coins.Quarter);
            CoinHelper.AddCoin(Coins.Quarter);
            CoinHelper.AddCoin(Coins.Dime);
            CoinHelper.AddCoin(Coins.Dime);
            CoinHelper.AddCoin(Coins.Dime);
            CoinHelper.AddCoin(Coins.Dime);
            CoinHelper.AddCoin(Coins.Nickel);
            CoinHelper.AddCoin(Coins.Nickel);
            CoinHelper.AddCoin(Coins.Nickel);
            CoinHelper.AddCoin(Coins.Nickel);

            // Act
            var result = _sut.SelectProduct(product);

            // Assert
            Assert.AreEqual(3, SessionHelper.ReturnCoins[Coins.Quarter]);
            Assert.AreEqual(2, SessionHelper.ReturnCoins[Coins.Dime]);
            Assert.AreEqual(.95M, SessionHelper.ReturnCoins.ToTotalValue());
            Assert.IsNotNull(result);
            Assert.AreEqual(MessageConstants.ThankYou, result);
        }

        [Test]
        public void SelectProduct_HavingSoldOutCandy_GivenCandy_ReturnsCoins()
        {
            // Arrange
            const Products product = Products.Candy;
            SessionHelper.Inventory[product] = 0;
            CoinHelper.AddCoin(Coins.Quarter);
            CoinHelper.AddCoin(Coins.Quarter);
            CoinHelper.AddCoin(Coins.Dime);
            CoinHelper.AddCoin(Coins.Nickel);

            // Act
            var result = _sut.SelectProduct(product);

            // Assert
            Assert.AreEqual(2, SessionHelper.CurrentCoins[Coins.Quarter]);
            Assert.AreEqual(1, SessionHelper.CurrentCoins[Coins.Dime]);
            Assert.AreEqual(1, SessionHelper.CurrentCoins[Coins.Nickel]);
            Assert.AreEqual(Products.Candy.ToValue(), SessionHelper.CurrentCoins.ToTotalValue());
            Assert.AreEqual(2, SessionHelper.TotalCoins[Coins.Quarter]);
            Assert.AreEqual(1, SessionHelper.TotalCoins[Coins.Dime]);
            Assert.AreEqual(1, SessionHelper.TotalCoins[Coins.Nickel]);
            Assert.AreEqual(Products.Candy.ToValue(), SessionHelper.TotalCoins.ToTotalValue());
            Assert.AreEqual(decimal.Zero, SessionHelper.ReturnCoins.ToTotalValue());
            Assert.IsNotNull(result);
            Assert.AreEqual(MessageConstants.SoldOut, result);
        }

        [Test]
        public void SelectProduct_HavingNoExactChange_GivenCandy_ReturnsExactChangeMessage()
        {
            // Arrange
            const Products product = Products.Candy;
            CoinHelper.AddCoin(Coins.Quarter);
            CoinHelper.AddCoin(Coins.Quarter);
            CoinHelper.AddCoin(Coins.Quarter);

            // Act
            var result = _sut.SelectProduct(product);

            // Assert
            Assert.AreEqual(3, SessionHelper.CurrentCoins[Coins.Quarter]);
            Assert.AreEqual(.75M, SessionHelper.CurrentCoins.ToTotalValue());
            Assert.AreEqual(3, SessionHelper.TotalCoins[Coins.Quarter]);
            Assert.AreEqual(.75M, SessionHelper.TotalCoins.ToTotalValue());
            Assert.AreEqual(decimal.Zero, SessionHelper.ReturnCoins.ToTotalValue());
            Assert.IsNotNull(result);
            Assert.AreEqual(MessageConstants.ExactChange, result);
        }

        #endregion
    }
}