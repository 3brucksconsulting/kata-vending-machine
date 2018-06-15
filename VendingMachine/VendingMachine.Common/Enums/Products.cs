using VendingMachine.Common.Attributes;

namespace VendingMachine.Common.Enums
{
    public enum Products
    {
        [Price("1.00")]
        Cola,

        [Price(".50")]
        Chips,

        [Price(".65")]
        Candy
    }
}