using System;
using System.Linq;
using VendingMachine.Common.Constants;
using VendingMachine.Common.Enums;
using VendingMachine.Common.Extensions;
using VendingMachine.Common.Helpers;

namespace VendingMachine.Services
{
    public class CoinService : ICoinService
    {
        #region Methods

        public string AcceptCoins(Coins? coin)
        {
            switch (coin)
            {
                case Coins.Penny:
                {
                    CoinHelper.AddReturnCoin(Coins.Penny);

                    break;
                }

                case Coins.Nickel:
                case Coins.Dime:
                case Coins.Quarter:
                {
                    CoinHelper.AddCoin(coin.Value);
                    
                    break;
                }
            }

            var totalValue = SessionHelper.CurrentCoins.TotalValue();

            return totalValue > decimal.Zero ? $"{totalValue:C2}" : MessageConstants.InsertCoin;
        }

        public string ReturnCoins()
        {
            foreach (var coin in Enum.GetValues(typeof(Coins)).Cast<Coins>())
            {
                // Skip pennies as they are already in return
                if (coin == Coins.Penny)
                {
                    continue;
                }

                SessionHelper.ReturnCoins[coin] = SessionHelper.CurrentCoins[coin];
                SessionHelper.CurrentCoins[coin] = 0;
            }

            return MessageConstants.InsertCoin;
        }

        #endregion
    }
}