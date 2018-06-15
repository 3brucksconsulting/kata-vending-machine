using VendingMachine.Common.Enums;

namespace VendingMachine.Common.Helpers
{
    public static class ProductHelper
    {
        #region Methods

        public static bool HasProduct(Products product)
        {
            return SessionHelper.Inventory[product] > 0;
        }

        public static void UpdateInventory(Products product)
        {
            SessionHelper.Inventory[product] = SessionHelper.Inventory[product] - 1;
        }
        
        #endregion
    }
}