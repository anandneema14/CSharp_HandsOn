namespace Interfaces;

public interface IChild : IBase
{
    void MethodChild();
    new void MethodOverride();
}