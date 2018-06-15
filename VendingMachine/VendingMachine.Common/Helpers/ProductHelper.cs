using VendingMachine.Common.Enums;

namespace VendingMachine.Common.Helpers
{
    public static class ProductHelper
    {
        #region Methods

        public static void UpdateInventory(Products product)
        {
            SessionHelper.Inventory[product] = SessionHelper.Inventory[product] - 1;
        }
        
        #endregion
    }
}