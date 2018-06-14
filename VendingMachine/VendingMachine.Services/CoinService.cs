using VendingMachine.Common.Enums;
using VendingMachine.Common.Helpers;

namespace VendingMachine.Services
{
    public class CoinService : ICoinService
    {
        #region Methods

        public void AcceptCoins(Coins? coin)
        {
            switch (coin)
            {
                case Coins.Penny:
                {
                    SessionHelper.AddReturnCoin(Coins.Penny);

                    break;
                }

                case Coins.Nickel:
                case Coins.Dime:
                case Coins.Quarter:
                {
                    SessionHelper.AddCoin(coin.Value);
                    
                    break;
                }
            }
        }

        #endregion
    }
}