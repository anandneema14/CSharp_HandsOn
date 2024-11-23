namespace Interfaces;

//Here we are implementing the Child Interface,
//But we need to implement Base Interface methods aa wel
//otherwise we will get the compilation error 
public class TestImplementation : IChild
{
    public void MethodChild()
    {
        Console.WriteLine("TestImplementation: Child Interface Method");
    }
    
    public void MethodBase1()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// This is Child Interface method Implementation
    /// </summary>
    void IChild.MethodOverride()
    {
        Console.WriteLine("TestImplementation: Child Interface Method");
    }

    /// <summary>
    /// This is Base Method Implementation
    /// </summary>
    void IBase.MethodOverride()
    {
        Console.WriteLine("TestImplementation: Base Interface Method");
    }
}