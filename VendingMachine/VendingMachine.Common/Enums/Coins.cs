using VendingMachine.Common.Attributes;

namespace VendingMachine.Common.Enums
{
    public enum Coins
    {
        [Value(".25")]
        Quarter,

        [Value(".10")]
        Dime,

        [Value(".05")]
        Nickel,

        [Value(".01")]
        Penny
    }
}