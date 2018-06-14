using VendingMachine.Common;
using VendingMachine.Common.Enums;

namespace VendingMachine.Services
{
    public interface ICoinService
    {
        #region Methods

        /// <summary>
        /// Accepts a given <paramref name="coin" /> and returns the <see cref="CoinBox" />.
        /// </summary>
        /// <param name="coin">The <see cref="Coins" />.</param>
        /// <returns>The <see cref="CoinBox" />.</returns>
        CoinBox AcceptCoins(Coins? coin);

        #endregion
    }
}