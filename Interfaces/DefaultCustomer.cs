namespace Interfaces;

/// <summary>
/// This way of declaring a class is called as Primary Constructor
/// </summary>
/// <param name="name"></param>
/// <param name="dateJoined"></param>
public class DefaultCustomer(string name, DateTime dateJoined) : ICustomer
{
    private List<IOrder> allOrders = new List<IOrder>();

    public IEnumerable<IOrder> PreviousOrders => allOrders;

    public DateTime DateJoined { get; } = dateJoined;

    public DateTime? LastOrder { get; private set; }

    public string Name { get; } = name;

    private Dictionary<DateTime, string> reminders = new Dictionary<DateTime, string>();
    public IDictionary<DateTime, string> Reminders => reminders;

    public void AddOrder(IOrder order)
    {
        if (order.Purchased > (LastOrder ?? DateTime.MinValue))
            LastOrder = order.Purchased;
        allOrders.Add(order);
    }
}