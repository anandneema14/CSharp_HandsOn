namespace AnonymousMethod_LambdaExpressions
{
    internal class Program
    {
        //Declaring Delegate
        delegate int del(int x, int y);
        static void Main(string[] args)
        {
            //Anonymous Method Implementation
            del d1 = delegate (int x, int y) { return x + y; };
            var result = d1(2, 3);
            Console.WriteLine(result);

            //Expression Lambda
            del d2 = (x, y) => x / y;
            var res2 = d2(6, 3);
            Console.WriteLine(res2);

            //Statement Lambda
            del d3 = (x, y) => { return x * y; };
            var res3 = d3(6, 3);
            Console.WriteLine(res3);

            Console.ReadKey();
        }
    }
}
