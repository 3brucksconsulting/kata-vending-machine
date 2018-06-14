﻿using System.Web;

namespace VendingMachine.Common
{
    public class CoinBox
    {
        #region Properties

        public double CurrentCoins
        {
            get
            {
                return Get<double>(Constants.CurrentCoins);
            }
            set
            {
                Set(Constants.CurrentCoins, value);
            }
        }

        public double TotalCoins
        {
            get
            {
                return Get<double>(Constants.TotalCoins);
            }
            set
            {
                Set(Constants.TotalCoins, value);
            }
        }

        #endregion

        #region Methods

        public void Clear()
        {
            HttpContext.Current.Session.Clear();
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

        #endregion

        #region Nested Members

        public static class Constants
        {
            public const string CurrentCoins = "CurrentCoins";
            public const string TotalCoins = "TotalCoins";
        }
        
        #endregion
    }
}