// See https://aka.ms/new-console-template for more information

using Interfaces;

TestImplementation test = new TestImplementation();
test.MethodChild();

IBase t2 = new TestImplementation();
t2.MethodOverride();

IChild t3 = new TestImplementation();
t3.MethodOverride();

Console.ReadLine();