using VendingMachine.Common.Constants;
using VendingMachine.Common.Enums;
using VendingMachine.Common.Extensions;
using VendingMachine.Common.Helpers;

namespace VendingMachine.Services
{
    public class ProductService : IProductService
    {
        #region Methods

        public string SelectProduct(Products product)
        {
            // Determine if product
            if (!ProductHelper.HasProduct(product))
            {
                return MessageConstants.SoldOut;
            }

            var currentCoins = SessionHelper.CurrentCoins.TotalValue();
            var price = product.Price();

            // Determine if no coins
            if (currentCoins == decimal.Zero)
            {
                return MessageConstants.InsertCoin;
            }

            // Determine if less than enough coins
            if (currentCoins < price)
            {
                return string.Format(MessageConstants.Price, product.Price());
            }

            // Determine if exact coins
            if (currentCoins == price)
            {
                ProductHelper.UpdateInventory(product);

                return MessageConstants.ThankYou;
            }

            // Calculate change
            var change = CoinHelper.CalculateChange(currentCoins, price);

            // Determine if exact change
            if (!CoinHelper.HasExactChange(change))
            {
                return MessageConstants.ExactChange;
            }

            // Determine if more than enough coins
            if (currentCoins > price)
            {
                ProductHelper.UpdateInventory(product);
                CoinHelper.MakeChange(change);

                return MessageConstants.ThankYou;
            }

            return string.Empty;
        }

        #endregion
    }
}