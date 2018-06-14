using System;

namespace VendingMachine.Common.Attributes
{
    public class ValueAttribute : Attribute
    {
        #region Fields

        private readonly double _value;

        #endregion

        #region Constructors
        
        public ValueAttribute(double value)
        {
            _value = value;
        }

        #endregion

        #region Methods

        public double ToValue()
        {
            return _value;
        }

        #endregion
    }
}