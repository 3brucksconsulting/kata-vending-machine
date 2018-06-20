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

        public static void AddCoin(Denominations coin)
        {
            // Increment current coins
            SessionHelper.CurrentCoins[coin] = SessionHelper.CurrentCoins[coin] + 1;

            // Increment total coins
            SessionHelper.TotalCoins[coin] = SessionHelper.TotalCoins[coin] + 1;
        }

        public static void AddReturnCoin(Denominations coin)
        {
            // Increment return coins
            SessionHelper.ReturnCoins[coin] = SessionHelper.ReturnCoins[coin] + 1;
        }

        public static Dictionary<Denominations, int> CalculateChange(decimal currentCoins, decimal price)
        {
            // Determine remainder
            var remainder = currentCoins - price;
            var change = new Dictionary<Denominations, int>();

            foreach (var coin in Enum.GetValues(typeof(Denominations)).Cast<Denominations>())
            {
                if (coin == Denominations.Unknown)
                {
                    change.Add(Denominations.Unknown, 0);
                }
                else
                {
                    // Calculate coin count
                    change.Add(coin, (int)(remainder / coin.Value()));

                    // Update remainder
                    remainder -= change[coin] * coin.Value();
                }
            }

            return change;
        }

        public static bool HasExactChange(Dictionary<Denominations, int> change)
        {
            // Determine if total coins has exact change
            return Enum.GetValues(typeof(Denominations))
                .Cast<Denominations>()
                .All(coin => SessionHelper.TotalCoins[coin] >= change[coin]);
        }

        public static void MakeChange(Dictionary<Denominations, int> change)
        {
            // Set return coins
            foreach (var coin in Enum.GetValues(typeof(Denominations)).Cast<Denominations>())
            {
                SessionHelper.ReturnCoins[coin] = change[coin];
            }
        }

        #endregion
    }
}