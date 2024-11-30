namespace Generics;

public class TestClass<T>
{
    private List<T> _list = new List<T>();
    
    public void Add(T item)
    {
        _list.Add(item);
    }

    public T Get(int index)
    {
        return _list[index];
    }
}