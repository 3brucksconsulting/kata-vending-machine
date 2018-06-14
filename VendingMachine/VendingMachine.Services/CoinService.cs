using VendingMachine.Common;
using VendingMachine.Common.Enums;

namespace VendingMachine.Services
{
    public class CoinService : ICoinService
    {
        public CoinBox AcceptCoins(Coins? coin)
        {
            var coinBox = new CoinBox();

            if (coin == Coins.Penny)
            {
                coinBox.AddReturnCoin(Coins.Penny);
            }

            return coinBox;
        }
    }
}