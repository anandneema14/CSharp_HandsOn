namespace Generics;

/// <summary>
/// Constraints specify the capabilities and expectations of a type parameter.
/// Declaring those constraints means you can use the operations and method calls of the constraining type.
/// You apply constraints to the type parameter when your generic class or method uses any operation
/// on the generic members beyond simple assignment,
/// which includes calling any methods not supported by System.Object. 
///
///where T : class CONSTRAINT
/// This constraint specifies that the type T must be a reference type (a class).
/// It prevents value types from being used as the generic argument.
/// </summary>
/// <typeparam name="T"></typeparam>
public class Generic_Constraints<T> where T : List<Employee>
{
    private class Node
    {
        public Node(T t) => (Next, Data) = (null, t);
        
        public Node? Next { get; set; }
        public T Data { get; set; }
    }
    private Node? head;

    public void AddHead(T t)
    {
        Node n = new Node(t) { Next = head };
        head = n;
    }
    
    public IEnumerator<T> GetEnumerator()
    {
        Node? current = head;

        while (current != null)
        {
            yield return current.Data;
            current = current.Next;
        }
    }
    
    public T? FindFirstOccurrence(string s)
    {
        Node? current = head;
        T? t = null;

        while (current != null)
        {
            //The constraint enables access to the Name property.
            foreach (var item in current.Data)
            {
                if (item.Name == s)
                {
                    t = current.Data;
                    break;
                }
                else
                {
                    current = current.Next;
                }
            }
        }
        return t;
    }
}