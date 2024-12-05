namespace AsyncSamples;

public class Button
{
    public Func<object, object, Task>? Clicked { get; internal set; }
}