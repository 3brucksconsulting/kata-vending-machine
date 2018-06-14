using System.Collections.Generic;
using System.Linq;
using VendingMachine.Common.Enums;

namespace VendingMachine.Common.Extensions
{
    /// <summary>
    /// Repository for dictionary-based extensions.
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Retrieves the total value for a given <paramref name="collection" />.
        /// </summary>
        /// <param name="collection">The <see cref="Dictionary{TKey,TValue}"/>.</param>
        /// <returns>The total value.</returns>
        public static decimal ToTotalValue(this Dictionary<Coins, int> collection)
        {
            return collection.Sum(x => x.Key.ToValue() * x.Value);
        }
    }
}