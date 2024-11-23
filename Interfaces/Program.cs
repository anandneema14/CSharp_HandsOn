// See https://aka.ms/new-console-template for more information

using Interfaces;

TestImplementation test = new TestImplementation();
test.MethodChild();

IBase t2 = new TestImplementation();
t2.MethodOverride();

IChild t3 = new TestImplementation();
t3.MethodOverride();

test.display_1();

//Accessing default implementation from Interface
Default_Interface_C_8_0 d1 =new TestImplementation();
d1.display_2();

Console.ReadLine();