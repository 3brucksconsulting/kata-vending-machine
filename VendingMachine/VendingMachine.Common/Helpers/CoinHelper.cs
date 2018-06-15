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
            // Increment current coins
            SessionHelper.CurrentCoins[coin] = SessionHelper.CurrentCoins[coin] + 1;

            // Increment total coins
            SessionHelper.TotalCoins[coin] = SessionHelper.TotalCoins[coin] + 1;
        }

        public static void AddReturnCoin(Coins coin)
        {
            // Increment return coins
            SessionHelper.ReturnCoins[coin] = SessionHelper.ReturnCoins[coin] + 1;
        }

        public static Dictionary<Coins, int> CalculateChange(decimal currentCoins, decimal price)
        {
            // Determine remainder
            var remainder = currentCoins - price;
            var change = new Dictionary<Coins, int>();

            foreach (var coin in Enum.GetValues(typeof(Coins)).Cast<Coins>())
            {
                // Calculate coin count
                change.Add(coin, (int)(remainder / coin.Value()));

                // Update remainder
                remainder -= change[coin] * coin.Value();
            }

            return change;
        }

        public static bool HasExactChange(Dictionary<Coins, int> change)
        {
            // Determine if total coins has exact change
            return Enum.GetValues(typeof(Coins))
                .Cast<Coins>()
                .All(coin => SessionHelper.TotalCoins[coin] >= change[coin]);
        }

        public static void MakeChange(Dictionary<Coins, int> change)
        {
            // Set return coins
            foreach (var coin in Enum.GetValues(typeof(Coins)).Cast<Coins>())
            {
                SessionHelper.ReturnCoins[coin] = change[coin];
            }
        }

        #endregion
    }
}