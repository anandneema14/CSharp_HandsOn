using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;

namespace AsyncSamples
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            #region Thread Example
            
            Console.WriteLine("--Sync Call: Thread Example");
            // wait for the method call to be completed
            var syncResult = ThreadExample.MyMethodSync(4);
            Console.WriteLine(syncResult);

            Console.WriteLine("--Async Call: Thread Example");
            // No wait for the method call to be completed
            var asyncResult = ThreadExample.MyMethodAsync(6);
            Console.WriteLine(asyncResult.Result);
            
            #endregion
            
            #region .NET Count on a particular web page
            //Async Program Code
            Console.WriteLine("Application started.");

            Console.WriteLine("Counting '.NET' phrase in websites...");
            int total = 0;
            try
            {
                
                foreach (string url in Constants.sUrlList)
                {
                    var result = await DotNETCount.GetDotNetCount(url);
                    Console.WriteLine($"{url}: {result}");
                    total += result;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("InCatch" + ex.Message);
            }

            Console.WriteLine("Total: " + total);

            Console.WriteLine("Retrieving User objects with list of IDs...");
            IEnumerable<int> ids = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };
            var users = await DotNETCount.GetUsersAsync(ids);
            foreach (User? user in users)
            {
                Console.WriteLine($"{user.Id}: isEnabled={user.IsEnabled}");
            }

            Console.WriteLine("Application ending.");
            
            #endregion

            #region Breakfast Example
            
            //Breakfast Example for simulating the Async operation
            Coffee cup = AsyncBreakfastExample.PourCoffee();
            Console.WriteLine("coffee is ready");

            var eggsTask = AsyncBreakfastExample.FryEggsAsync(2);
            var baconTask = AsyncBreakfastExample.FryBaconAsync(3);
            var toastTask = AsyncBreakfastExample.MakeToastWithButterAndJamAsync(2);

            var breakfastTasks = new List<Task> { eggsTask, baconTask, toastTask };
            while (breakfastTasks.Count > 0)
            {
                Task finishedTask = await Task.WhenAny(breakfastTasks);
                if (finishedTask == eggsTask)
                {
                    Console.WriteLine("eggs are ready");
                }
                else if (finishedTask == baconTask)
                {
                    Console.WriteLine("bacon is ready");
                }
                else if (finishedTask == toastTask)
                {
                    Console.WriteLine("toast is ready");
                }
                await finishedTask;
                breakfastTasks.Remove(finishedTask);
            }

            Juice oj = AsyncBreakfastExample.PourOJ();
            Console.WriteLine("Orange Juice is ready");
            Console.WriteLine("Breakfast is ready!");
            
            #endregion

            #region Async Return Types
            
            Console.WriteLine("--Async Return Types");
            
            //Task.GetAwaiter().GetResult() is preferred over Task.Wait and Task.Result
            //because it propagates exceptions rather than wrapping them in an AggregateException.
            //However, all these methods cause potential deadlock and thread pool starvation issues.
            //They should all be avoided in favor of async/await
            var resultAsync2 = AsyncReturnTypes.MethodAsync2(5);
            resultAsync2.Wait();
            resultAsync2.GetAwaiter().GetResult();
            
            //AsyncReturnTypes asyncReturnTypes = new AsyncReturnTypes();
            await foreach (var a in AsyncReturnTypes.ReadWordsFromStreamAsync())
            {
                Console.WriteLine(a);
            }
            
            #endregion
            
            #region Process Async Tasks as they Complete

            await ProcessAsyncTasks.SumPageSizesAsync();
            
            #endregion
            
            #region Accessing Files Asynchronously
            
            Console.WriteLine("--Writing Files Asynchronously");
            await AsyncFileAccess.ProcessWriteAsync($"sample.txt");
            Console.WriteLine("--Data Written completed");
            
            Console.WriteLine("--Reading Files Asynchronously");
            await AsyncFileAccess.ProcessReadAsync($"sample.txt");
            Console.WriteLine("--Data Read completed");
            
            #endregion
            
            Console.WriteLine("This is a test comment for checkin to git via MAC");
            Console.ReadLine();
        }
    }
}
