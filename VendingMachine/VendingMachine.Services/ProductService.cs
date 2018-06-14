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

            if (SessionHelper.CurrentCoins.ToTotalValue() < product.ToValue())
            {
                message = string.Format(MessageConstants.Price, product.ToValue());
            }

            return message;
        }

        #endregion
    }
}