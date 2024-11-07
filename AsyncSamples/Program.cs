namespace AsyncSamples
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--Sync Call: Thread Example");
            // wait for the method call to be completed
            var syncResult = ThreadExample.MyMethodSync(4);
            Console.WriteLine(syncResult);

            Console.WriteLine("--Async Call: Thread Example");
            // No wait for the method call to be completed
            var asyncResult = ThreadExample.MyMethodAsync(6);
            Console.WriteLine(asyncResult.Result);

            Console.WriteLine("--Async Return Types");
            
            ///Task.GetAwaiter().GetResult() is preferred over Task.Wait and Task.Result
            ///because it propagates exceptions rather than wrapping them in an AggregateException.
            ///However, all these methods cause potential deadlock and thread pool starvation issues.
            ///They should all be avoided in favor of async/await
            var resultAsync2 = AsyncReturnType.MethodAsync2(5);
            resultAsync2.Wait();
            resultAsync2.GetAwaiter().GetResult();


            Console.ReadLine();
        }
    }
}
