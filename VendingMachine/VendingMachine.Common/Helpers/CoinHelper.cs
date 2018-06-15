using System;
using System.Linq;
using VendingMachine.Common.Enums;
using VendingMachine.Common.Extensions;

namespace VendingMachine.Common.Helpers
{
    public static class CoinHelper
    {
        #region Methods

        public static void MakeChange(decimal totalCoins, decimal price)
        {
            var change = totalCoins - price;

            foreach (var coin in Enum.GetValues(typeof(Coins)).Cast<Coins>())
            {
                SessionHelper.ReturnCoins[coin] = (int)(change / coin.ToValue());

                change -= SessionHelper.ReturnCoins[coin] * coin.ToValue();
            }
        }

        #endregion
    }
}