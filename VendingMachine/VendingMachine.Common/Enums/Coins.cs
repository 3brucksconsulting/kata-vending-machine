using VendingMachine.Common.Attributes;

namespace VendingMachine.Common.Enums
{
    public enum Coins
    {
        [Value(.01)]
        Penny,

        [Value(.05)]
        Nickel,

        [Value(.10)]
        Dime,

        [Value(.25)]
        Quarter
    }
}