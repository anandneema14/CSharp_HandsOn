namespace Interfaces;

public interface IOrder
{
    DateTime Purchased { get; }
    decimal Cost { get; }
}