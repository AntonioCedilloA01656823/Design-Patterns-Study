// Extra C# example of Strategy (same checkout problem as example.ipynb).
// Run: dotnet new console -n StrategyDemo -o /tmp/StrategyDemo --force
//      then replace Program.cs with this file, or compile it as a top-level program.

using System;

interface IShippingStrategy
{
    decimal Cost(double weightKg);
}

sealed class StandardShipping : IShippingStrategy
{
    public decimal Cost(double weightKg) => 5.00m + 1.25m * (decimal)weightKg;
}

sealed class ExpressShipping : IShippingStrategy
{
    public decimal Cost(double weightKg) => 12.00m + 2.50m * (decimal)weightKg;
}

sealed class OvernightShipping : IShippingStrategy
{
    public decimal Cost(double weightKg) => 25.00m + 4.00m * (decimal)weightKg;
}

sealed class Checkout
{
    private IShippingStrategy _shipping;

    public Checkout(IShippingStrategy shipping) => _shipping = shipping;

    public void SetShipping(IShippingStrategy shipping) => _shipping = shipping;

    public decimal Total(decimal subtotal, double weightKg) =>
        decimal.Round(subtotal + _shipping.Cost(weightKg), 2);
}

var checkout = new Checkout(new StandardShipping());
Console.WriteLine($"Standard: {checkout.Total(40.00m, 2.0)}");

checkout.SetShipping(new ExpressShipping());
Console.WriteLine($"Express: {checkout.Total(40.00m, 2.0)}");

checkout.SetShipping(new OvernightShipping());
Console.WriteLine($"Overnight: {checkout.Total(40.00m, 2.0)}");
