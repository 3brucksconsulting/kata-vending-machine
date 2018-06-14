using VendingMachine.Common;
using VendingMachine.Common.Enums;

namespace VendingMachine.Services
{
    public class CoinService : ICoinService
    {
        public CoinBox AcceptCoins(Coins? coin)
        {
            return new CoinBox();
        }
    }
}