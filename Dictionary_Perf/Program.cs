// See https://aka.ms/new-console-template for more information
using Dictionary_Perf;
using System.Text.Json;

var values = new Dictionary<int, string>
{
    {24424,"Entry1" }
};

//Time complexity of using a dictionary is always O(1), meaning its constant time

//to check the value before inserting it,
//we need to traverse whole dictionary,
//we are doing same hash operation multiple time as described in the code below
 
if (values.ContainsKey(24424))
{
    values[24424] = "Test";
}

//Effective way
var val = values.GetOrAdd(24424, "Test");
Console.WriteLine(val);

val = values.GetOrAdd(3452, "Test");
Console.WriteLine(val);

values.TryUpdate(24424, "Entry2");
Console.WriteLine(JsonSerializer.Serialize(values));
Console.ReadLine();
