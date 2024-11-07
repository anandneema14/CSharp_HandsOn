namespace AsyncSamples
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--Sync Call");
            // wait for the method call to be completed
            var syncResult = ThreadExample.MyMethodSync(4);
            Console.WriteLine(syncResult);

            Console.WriteLine("--Async Call");
            // No wait for the method call to be completed
            var asyncResult = ThreadExample.MyMethodAsync(6);
            Console.WriteLine(asyncResult.Result);

            Console.ReadLine();
        }
    }
}
