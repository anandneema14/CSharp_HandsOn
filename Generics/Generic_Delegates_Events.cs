namespace Generics;

public delegate void EventHandler<TEventArgs>(object sender, TEventArgs e);


/// <summary>
/// In this example, the EventHandler<TEventArgs> delegate is a generic delegate
/// that can handle events with different event argument types.
/// The Generic_Delegates_Events class uses this generic delegate to handle the Click event
/// with a custom event argument type ClickEventArgs
/// </summary>
public class Generic_Delegates_Events
{
    public event EventHandler<ClickEventArgs> Click;

    public void SimulateClick()
    {
        // Raise the Click event
        Click?.Invoke(this, new ClickEventArgs());
    }
}

public class ClickEventArgs
{
    public DateTime ClickTime { get; } = DateTime.Now;
}

