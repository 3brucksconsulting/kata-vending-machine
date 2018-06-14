using VendingMachine.Common.Attributes;

namespace VendingMachine.Common.Enums
{
    public enum Products
    {
        [Value("1.00")]
        Cola,

        [Value(".50")]
        Chips,

        [Value(".65")]
        Candy
    }
}