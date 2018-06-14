using VendingMachine.Common;
using VendingMachine.Common.Enums;

namespace VendingMachine.Services
{
    public class CoinService : ICoinService
    {
        public CoinBox AcceptCoins(Coins? coin)
        {
            var coinBox = new CoinBox();

            switch (coin)
            {
                case Coins.Penny:
                {
                    coinBox.AddReturnCoin(Coins.Penny);

                    break;
                }

                case Coins.Nickel:
                case Coins.Dime:
                case Coins.Quarter:
                {
                    coinBox.AddCurrentCoin(coin.Value);
                    coinBox.AddTotalCoin(coin.Value);

                    break;
                }
            }

            return coinBox;
        }
    }
}