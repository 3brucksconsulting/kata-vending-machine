using System;
using VendingMachine.Common.Attributes;

namespace VendingMachine.Common.Extensions
{
    /// <summary>
    /// Repository for enum-based extensions.
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Converts an <see cref="Enum" /> value to its equivalant price if available.
        /// </summary>
        /// <param name="value">The <see cref="Enum" /> value.</param>
        /// <returns>The price if set; Otherwise zero.</returns>
        public static decimal Price(this Enum value)
        {
            var type = value.GetType();
            var field = type.GetField(value.ToString());
            var attributes = field.GetCustomAttributes(typeof(PriceAttribute), false);

            return attributes.Length == 0
                ? decimal.Zero
                : ((PriceAttribute)attributes[0]).ToValue();
        }

        /// <summary>
        /// Converts an <see cref="Enum" /> value to its equivalant value if available.
        /// </summary>
        /// <param name="value">The <see cref="Enum" /> value.</param>
        /// <returns>The value if set; Otherwise zero.</returns>
        public static decimal Value(this Enum value)
        {
            var type = value.GetType();
            var field = type.GetField(value.ToString());
            var attributes = field.GetCustomAttributes(typeof(ValueAttribute), false);

            return attributes.Length == 0
                ? decimal.Zero
                : ((ValueAttribute)attributes[0]).ToValue();
        }
    }
}