using VendingMachine.Common.Enums;

namespace VendingMachine.Services
{
    public interface ICoinService
    {
        #region Methods

        /// <summary>
        /// Accepts the given <paramref name="coin" /> and returns the appropriate message.
        /// </summary>
        /// <param name="coin">The <see cref="Coins" />.</param>
        /// <returns>The current coin amount if not empty; Otherwise INSERT COIN.</returns>
        string AcceptCoins(Coins? coin);

        #endregion
    }
}