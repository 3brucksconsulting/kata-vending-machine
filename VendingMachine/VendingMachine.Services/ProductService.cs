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
            var message = string.Empty;
            var coins = SessionHelper.CurrentCoins.ToTotalValue();
            var price = product.ToValue();

            if (coins == price)
            {
                SessionHelper.UpdateInventory(product);

                message = MessageConstants.ThankYou;
            }
            else if (coins < price)
            {
                message = string.Format(MessageConstants.Price, product.ToValue());
            }

            return message;
        }

        #endregion
    }
}