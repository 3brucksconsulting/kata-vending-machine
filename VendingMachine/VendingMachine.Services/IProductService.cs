using VendingMachine.Common.Enums;

namespace VendingMachine.Services
{
    public interface IProductService
    {
        #region Methods

        /// <summary>
        /// Selects a given <paramref name="product" /> returns the appropriate message.
        /// </summary>
        // <returns>The appropriate message.</returns>
        string SelectProduct(Products product);

        #endregion
    }
}