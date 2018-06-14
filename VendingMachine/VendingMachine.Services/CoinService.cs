using VendingMachine.Common;
using VendingMachine.Common.Enums;

namespace VendingMachine.Services
{
    public class CoinService : ICoinService
    {
        #region Fields

        private CoinBox _coinBox;

        #endregion

        #region Constructors

        public CoinService()
        {
            _coinBox = new CoinBox();
        }

        #endregion

        #region Methods

        public CoinBox AcceptCoins(Coins? coin)
        {
            switch (coin)
            {
                case Coins.Penny:
                {
                    _coinBox.AddReturnCoin(Coins.Penny);

                    break;
                }

                case Coins.Nickel:
                case Coins.Dime:
                case Coins.Quarter:
                {
                    _coinBox.AddCurrentCoin(coin.Value);
                    _coinBox.AddTotalCoin(coin.Value);

                    break;
                }
            }

            return _coinBox;
        }

        #endregion
    }
}