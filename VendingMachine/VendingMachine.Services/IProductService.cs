using VendingMachine.Common.Enums;

namespace VendingMachine.Services
{
    public interface IProductService
    {
        #region Methods

        /// <summary>
        /// Selects a given <paramref name="product" />.
        /// </summary>
        /// <param name="product">The <see cref="Products" />.</param>
        void SelectProduct(Products product);

        #endregion
    }
}