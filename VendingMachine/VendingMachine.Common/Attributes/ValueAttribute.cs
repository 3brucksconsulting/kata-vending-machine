using System;

namespace VendingMachine.Common.Attributes
{
    public class ValueAttribute : Attribute
    {
        #region Fields

        private readonly decimal _value;

        #endregion

        #region Constructors
        
        public ValueAttribute(string value)
        {
            _value = decimal.Parse(value);
        }

        #endregion

        #region Methods

        public decimal ToValue()
        {
            return _value;
        }

        #endregion
    }
}