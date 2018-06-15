using System;
using System.Collections.Generic;
using System.Linq;
using VendingMachine.Common.Enums;
using VendingMachine.Common.Extensions;

namespace VendingMachine.Common.Helpers
{
    public static class CoinHelper
    {
        #region Methods

        public static void AddCoin(Coins coin)
        {
            SessionHelper.CurrentCoins[coin] = SessionHelper.CurrentCoins[coin] + 1;
            SessionHelper.TotalCoins[coin] = SessionHelper.TotalCoins[coin] + 1;
        }

        public static void AddReturnCoin(Coins coin)
        {
            SessionHelper.ReturnCoins[coin] = SessionHelper.ReturnCoins[coin] + 1;
        }

        public static Dictionary<Coins, int> CalculateChange(decimal currentCoins, decimal price)
        {
            var remainder = currentCoins - price;
            var change = new Dictionary<Coins, int>();

            foreach (var coin in Enum.GetValues(typeof(Coins)).Cast<Coins>())
            {
                change.Add(coin, (int)(remainder / coin.ToValue()));

                remainder -= change[coin] * coin.ToValue();
            }

            return change;
        }

        public static bool HasExactChange(Dictionary<Coins, int> change)
        {
            return Enum.GetValues(typeof(Coins))
                .Cast<Coins>()
                .All(coin => SessionHelper.TotalCoins[coin] >= change[coin]);
        }

        public static void MakeChange(Dictionary<Coins, int> change)
        {
            foreach (var coin in Enum.GetValues(typeof(Coins)).Cast<Coins>())
            {
                SessionHelper.ReturnCoins[coin] = change[coin];
            }
        }

        #endregion
    }
}