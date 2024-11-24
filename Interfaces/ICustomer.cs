namespace Interfaces;

public interface ICustomer
{
    IEnumerable<IOrder> PreviousOrders { get; }

    DateTime DateJoined { get; }
    DateTime? LastOrder { get; }
    string Name { get; }
    IDictionary<DateTime, string> Reminders { get; }
    
    // Version 1:
    public decimal ComputeLoyaltyDiscount()
    {
        DateTime twoYearsAgo = DateTime.Now.AddYears(-2);
        if ((DateJoined < twoYearsAgo) && (PreviousOrders.Count() > 10))
        {
            return 0.10m;
        }
        return 0;
    }
}