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
Default_Interface_C_8_0 d1 = new TestImplementation();
d1.display_2();


DefaultCustomer defaultCustomer = new DefaultCustomer("customer one", new DateTime(2010, 5, 31))
{
    Reminders =
    {
        { new DateTime(2024, 08, 12), "childs's birthday" },
        { new DateTime(2012, 11, 15), "anniversary" }
    }
};

DefaultOrder defaultOrder = new DefaultOrder(new DateTime(2012, 6, 1), 5m);
defaultCustomer.AddOrder(defaultOrder);

defaultOrder = new DefaultOrder(new DateTime(2013, 7, 4), 25m);
defaultCustomer.AddOrder(defaultOrder);

// Check the discount:
ICustomer theCustomer = defaultCustomer;
Console.WriteLine($"Current discount: { theCustomer.ComputeLoyaltyDiscount() }");

//New Overrided Implementation
NewCustomer newCustomer = new NewCustomer("new customer", new DateTime(2010, 5, 31));
newCustomer.AddOrder(defaultOrder);

ICustomer customer = newCustomer;
Console.WriteLine($"Current discount: { customer.ComputeLoyaltyDiscount() }");

Console.ReadLine();