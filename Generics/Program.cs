// See https://aka.ms/new-console-template for more information

using Generics;

TestClass<int> testClass = new TestClass<int>();
testClass.Add(1);
testClass.Add(2);
testClass.Add(3);

Console.WriteLine(testClass.Get(2));

TestClass<string> testString = new TestClass<string>();
testString.Add("Anand");
testString.Add("Aarav");
testString.Add("Vidhi");
testString.Add("Gunnu");

Console.WriteLine(testString.Get(1));

//Generic Delegates Example
Generic_Delegates_Events testDelegates = new Generic_Delegates_Events();
testDelegates.Click += (sender, e) =>
{
    Console.WriteLine("Button Clicked at: " + e.ClickTime);
};

testDelegates.SimulateClick();

List<Employee> e1 = new List<Employee>();

e1.Add(new Employee(1, "John Doe"));
e1.Add(new Employee(2, "Jane Doe"));
e1.Add(new Employee(3, "Aarvi Neema"));
e1.Add(new Employee(4, "Aarav Neema"));
e1.Add(new Employee(5, "Vidhi Neema"));
e1.Add(new Employee(6, "Anand Neema"));


Generic_Constraints<List<Employee>> testGenericsConstraints = new Generic_Constraints<List<Employee>>();
testGenericsConstraints.AddHead(e1);

//var result = testGenericsConstraints.GetEnumerator();

foreach (var e in testGenericsConstraints)
{
    foreach (var t in e)
    {
        Console.WriteLine(t.Name.ToString());
    }
}

Console.ReadLine();
