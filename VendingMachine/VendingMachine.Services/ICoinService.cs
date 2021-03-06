﻿using VendingMachine.Common.Classes;

namespace VendingMachine.Services
{
    public interface ICoinService
    {
        #region Methods

        /// <summary>
        /// Accepts the given <paramref name="coin" /> and returns the appropriate message.
        /// </summary>
        /// <param name="coin">The <see cref="Coin" />.</param>
        /// <returns>The current coin amount if not empty; Otherwise INSERT COIN.</returns>
        string AcceptCoins(Coin coin);

        /// <summary>
        /// Moves all current coins to the return coins and returns the appropriate message.
        /// </summary>
        /// <returns>The INSERT COIN message.</returns>
        string ReturnCoins();

        #endregion
    }
}