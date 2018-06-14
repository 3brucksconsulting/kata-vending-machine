using System.Web;
using VendingMachine.Common.Enums;
using VendingMachine.Common.Extensions;

namespace VendingMachine.Common
{
    public class CoinBox
    {
        #region Properties

        public decimal CurrentCoins
        {
            get
            {
                return Get<decimal>(Constants.CurrentCoins);
            }
            set
            {
                Set(Constants.CurrentCoins, value);
            }
        }

        public string Message
        {
            get
            {
                if (CurrentCoins == decimal.Zero)
                {
                    return Constants.InsertCoin;
                }

                return string.Empty;
            }
        }

        public decimal ReturnCoins
        {
            get
            {
                return Get<decimal>(Constants.ReturnCoins);
            }
            set
            {
                Set(Constants.ReturnCoins, value);
            }
        }

        public decimal TotalCoins
        {
            get
            {
                return Get<decimal>(Constants.TotalCoins);
            }
            set
            {
                Set(Constants.TotalCoins, value);
            }
        }

        #endregion

        #region Methods

        public void AddCurrentCoin(Coins coin)
        {
            CurrentCoins = CurrentCoins + coin.ToValue();
        }

        public void AddReturnCoin(Coins coin)
        {
            ReturnCoins = ReturnCoins + coin.ToValue();
        }

        public void AddTotalCoin(Coins coin)
        {
            TotalCoins = TotalCoins + coin.ToValue();
        }

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
            public const string InsertCoin = "INSERT COIN";
            public const string ReturnCoins = "ReturnCoins";
            public const string TotalCoins = "TotalCoins";
        }
        
        #endregion
    }
}