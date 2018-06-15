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

        public static void AddCoin(Coins coin)
        {
            CurrentCoins[coin] = CurrentCoins[coin] + 1;
            TotalCoins[coin] = TotalCoins[coin] + 1;
        }

        public static void AddReturnCoin(Coins coin)
        {
            ReturnCoins[coin] = ReturnCoins[coin] + 1;
        }

        public static void ClearAll()
        {
            HttpContext.Current.Session.Clear();
        }

        public static void ClearCurrent()
        {
            HttpContext.Current.Session.Remove(SessionConstants.CurrentCoins);
            HttpContext.Current.Session.Remove(SessionConstants.ReturnCoins);
        }
        
        public static T Get<T>(string key)
        {
            object value = null;

            if (HttpContext.Current.Session[key] != null)
            {
                value = HttpContext.Current.Session[key];
            }

            return value != null ? (T)value : default(T);
        }

        public static void Set(string key, object value)
        {
            HttpContext.Current.Session[key] = value;
        }

        public static void UpdateInventory(Products product)
        {
            Inventory[product] = Inventory[product] - 1;
        }

        private static Dictionary<TEnum, int> InitializeDictionary<TEnum>(int value)
        {
            return Enum.GetValues(typeof(TEnum))
                .Cast<TEnum>()
                .ToDictionary(x => x, y => value);
        }

        #endregion
    }
}