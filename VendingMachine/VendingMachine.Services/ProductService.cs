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
            var message = CheckInventory(product);

            if (message == string.Empty)
            {
                message = CheckCoins(product);
            }

            return message;
        }

        private static string CheckCoins(Products product)
        {
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

        private static string CheckInventory(Products product)
        {
            return ProductHelper.HasProduct(product) ? string.Empty : MessageConstants.SoldOut;
        }

        #endregion
    }
}