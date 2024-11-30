namespace Generics;

public class Employee
{
    public Employee(int id, string name) => (ID, Name) = (id, name);
    public string Name { get; set; }
    public int ID { get; set; }
}