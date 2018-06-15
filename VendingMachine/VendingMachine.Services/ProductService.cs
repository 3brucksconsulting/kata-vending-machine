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
            var message = MessageConstants.SoldOut;

            if (!ProductHelper.HasProduct(product))
            {
                return message;
            }
            
            var currentCoins = SessionHelper.CurrentCoins.ToTotalValue();
            var price = product.ToValue();

            if (currentCoins == decimal.Zero)
            {
                message = MessageConstants.InsertCoin;
            }
            else if (currentCoins == price)
            {
                ProductHelper.UpdateInventory(product);
                SessionHelper.ClearCurrent();

                message = MessageConstants.ThankYou;
            }
            else if (currentCoins > price)
            {
                ProductHelper.UpdateInventory(product);
                SessionHelper.ClearCurrent();

                CoinHelper.MakeChange(currentCoins, price);

                message = MessageConstants.ThankYou;
            }
            else if (currentCoins < price)
            {
                message = string.Format(MessageConstants.Price, product.ToValue());
            }
            
            return message;
        }

        #endregion
    }
}