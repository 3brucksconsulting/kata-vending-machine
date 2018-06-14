using VendingMachine.Common.Enums;

namespace VendingMachine.Services
{
    public interface ICoinService
    {
        #region Methods

        /// <summary>
        /// Accepts a given <paramref name="coin" />.
        /// </summary>
        /// <param name="coin">The <see cref="Coins" />.</param>
        void AcceptCoins(Coins? coin);

        #endregion
    }
}