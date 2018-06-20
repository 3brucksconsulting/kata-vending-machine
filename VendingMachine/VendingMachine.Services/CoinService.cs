using System;
using System.Linq;
using VendingMachine.Common.Classes;
using VendingMachine.Common.Constants;
using VendingMachine.Common.Enums;
using VendingMachine.Common.Extensions;
using VendingMachine.Common.Helpers;

namespace VendingMachine.Services
{
    public class CoinService : ICoinService
    {
        #region Methods

        public string AcceptCoins(Coin coin)
        {
            coin.Denomination = DetermineDenomination(coin);

            switch (coin.Denomination)
            {
                case Denominations.Penny:
                case Denominations.Unknown:
                {
                    CoinHelper.AddReturnCoin(coin.Denomination);

                    break;
                }

                case Denominations.Nickel:
                case Denominations.Dime:
                case Denominations.Quarter:
                {
                    CoinHelper.AddCoin(coin.Denomination);

                    break;
                }
            }

            var totalValue = SessionHelper.CurrentCoins.TotalValue();

            return totalValue > decimal.Zero ? $"{totalValue:C2}" : MessageConstants.InsertCoin;
        }

        public string ReturnCoins()
        {
            foreach (var coin in Enum.GetValues(typeof(Denominations)).Cast<Denominations>())
            {
                // Skip pennies and unknown coins as they are already in return
                if (coin == Denominations.Penny || coin == Denominations.Unknown)
                {
                    continue;
                }

                SessionHelper.ReturnCoins[coin] = SessionHelper.CurrentCoins[coin];
                SessionHelper.TotalCoins[coin] = SessionHelper.TotalCoins[coin] - SessionHelper.CurrentCoins[coin];
                SessionHelper.CurrentCoins[coin] = 0;
            }

            return MessageConstants.InsertCoin;
        }

        private static Denominations DetermineDenomination(Coin coin)
        {
            var denomination = Denominations.Unknown;

            if (coin.Weight == WeightConstants.Quarter && coin.Diameter == DiameterConstants.Quarter)
            {
                denomination = Denominations.Quarter;
            }
            else if (coin.Weight == WeightConstants.Dime && coin.Diameter == DiameterConstants.Dime)
            {
                denomination = Denominations.Dime;
            }
            else if (coin.Weight == WeightConstants.Nickle && coin.Diameter == DiameterConstants.Nickle)
            {
                denomination = Denominations.Nickel;
            }
            else if (coin.Weight == WeightConstants.Penny && coin.Diameter == DiameterConstants.Penny)
            {
                denomination = Denominations.Penny;
            }

            return denomination;
        }

        #endregion
    }
}