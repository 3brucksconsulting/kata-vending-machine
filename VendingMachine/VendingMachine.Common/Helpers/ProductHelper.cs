using VendingMachine.Common.Enums;

namespace VendingMachine.Common.Helpers
{
    public static class ProductHelper
    {
        #region Methods

        public static bool HasProduct(Products product)
        {
            // Detemine if product available
            return SessionHelper.Inventory[product] > 0;
        }

        public static void UpdateInventory(Products product)
        {
            // Decrement product count
            SessionHelper.Inventory[product] = SessionHelper.Inventory[product] - 1;

            SessionHelper.ClearCurrent();
        }
        
        #endregion
    }
}