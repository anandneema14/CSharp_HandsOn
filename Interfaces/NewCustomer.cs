namespace Interfaces;

public class NewCustomer(string name, DateTime dateJoined) : ICustomer
{
    private List<IOrder> allOrders = new List<IOrder>();
    public DateTime DateJoined { get; } = dateJoined;

    public string Name { get; } = name;

    public IEnumerable<IOrder> PreviousOrders => allOrders;
    
    public DateTime? LastOrder { get; private set; }
    
    private Dictionary<DateTime, string> reminders = new Dictionary<DateTime, string>();
    
    public IDictionary<DateTime, string> Reminders => reminders;

    public decimal ComputeLoyaltyDiscount()
    {
        DateTime twoYearsAgo = DateTime.Now.AddYears(-5);
        if ((DateJoined < twoYearsAgo) && (PreviousOrders.Count() > 3))
        {
            return 0.20m;
        }
        return 0;
    }
    
    public void AddOrder(IOrder order)
    {
        if (order.Purchased > (LastOrder ?? DateTime.MinValue))
            LastOrder = order.Purchased;
        allOrders.Add(order);
    }
}