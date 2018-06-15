using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VendingMachine.Common.Constants;
using VendingMachine.Common.Enums;

namespace VendingMachine.Common.Helpers
{
    public static class SessionHelper
    {
        #region Properties

        public static Dictionary<Coins, int> CurrentCoins
        {
            get
            {
                if (Get<Dictionary<Coins, int>>(SessionConstants.CurrentCoins) == null)
                {
                    Set(SessionConstants.CurrentCoins, InitializeDictionary<Coins>(0));
                }

                return Get<Dictionary<Coins, int>>(SessionConstants.CurrentCoins);
            }
        }

        public static Dictionary<Products, int> Inventory
        {
            get
            {
                if (Get<Dictionary<Products, int>>(SessionConstants.Inventory) == null)
                {
                    Set(SessionConstants.Inventory, InitializeDictionary<Products>(3));
                }

                return Get<Dictionary<Products, int>>(SessionConstants.Inventory);
            }

        }

        public static Dictionary<Coins, int> ReturnCoins
        {
            get
            {
                if (Get<Dictionary<Coins, int>>(SessionConstants.ReturnCoins) == null)
                {
                    Set(SessionConstants.ReturnCoins, InitializeDictionary<Coins>(0));
                }

                return Get<Dictionary<Coins, int>>(SessionConstants.ReturnCoins);
            }
        }

        public static Dictionary<Coins, int> TotalCoins
        {
            get
            {
                if (Get<Dictionary<Coins, int>>(SessionConstants.TotalCoins) == null)
                {
                    Set(SessionConstants.TotalCoins, InitializeDictionary<Coins>(0));
                }

                return Get<Dictionary<Coins, int>>(SessionConstants.TotalCoins);
            }

        }

        #endregion

        #region Methods

        public static void ClearAll()
        {
            // Clear session
            HttpContext.Current.Session.Clear();
        }

        public static void ClearCurrent()
        {
            // Clear current coins
            HttpContext.Current.Session.Remove(SessionConstants.CurrentCoins);

            // Clear return coins
            HttpContext.Current.Session.Remove(SessionConstants.ReturnCoins);
        }
        
        public static T Get<T>(string key)
        {
            object value = null;

            // attempt to retrieve item value
            if (HttpContext.Current.Session[key] != null)
            {
                value = HttpContext.Current.Session[key];
            }

            return value != null ? (T)value : default(T);
        }

        public static void Set(string key, object value)
        {
            // Set item value
            HttpContext.Current.Session[key] = value;
        }

        private static Dictionary<TEnum, int> InitializeDictionary<TEnum>(int value)
        {
            // Initialize collection
            return Enum.GetValues(typeof(TEnum))
                .Cast<TEnum>()
                .ToDictionary(x => x, y => value);
        }

        #endregion
    }
}