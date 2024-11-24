namespace Interfaces;

public class DefaultOrder : IOrder
{
    public DefaultOrder(DateTime purchase, decimal cost) =>
        (Purchased, Cost) = (purchase, cost);

    public DateTime Purchased { get; }

    public decimal Cost { get; }
}