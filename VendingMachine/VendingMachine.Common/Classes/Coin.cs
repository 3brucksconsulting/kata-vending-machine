using VendingMachine.Common.Enums;

namespace VendingMachine.Common.Classes
{
    public sealed class Coin
    {
        #region Constructors

        public Coin(decimal weight, decimal diameter)
        {
            Weight = weight;
            Diameter = diameter;
        }

        #endregion

        #region Properties

        public Denominations Denomination { get; set; }

        public decimal Diameter { get; }
        
        public decimal Weight { get; }

        #endregion
    }
}