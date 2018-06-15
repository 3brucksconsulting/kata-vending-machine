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

            var totalValue = SessionHelper.CurrentCoins.ToTotalValue();

            return totalValue > decimal.Zero ? $"{totalValue:C2}" : MessageConstants.InsertCoin;
        }

        #endregion
    }
}