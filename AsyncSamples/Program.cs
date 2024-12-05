using System.Text.RegularExpressions;
using System.Windows;
using Microsoft.AspNetCore.Mvc;

namespace AsyncSamples
{
    public class Program
    {
        //Code for Async Methods
        private static readonly Button s_downloadButton = new();
        private static readonly Button s_calculateButton = new();

        private static readonly HttpClient s_httpClient = new();
        
        private static readonly IEnumerable<string> s_urlList = new string[]
        {
            "https://learn.microsoft.com",
            "https://learn.microsoft.com/aspnet/core",
            "https://learn.microsoft.com/azure",
            "https://learn.microsoft.com/azure/devops",
            "https://learn.microsoft.com/dotnet",
            "https://learn.microsoft.com/dotnet/desktop/wpf/get-started/create-app-visual-studio",
            "https://learn.microsoft.com/education",
            "https://learn.microsoft.com/shows/net-core-101/what-is-net",
            "https://learn.microsoft.com/enterprise-mobility-security",
            "https://learn.microsoft.com/gaming",
            "https://learn.microsoft.com/graph",
            "https://learn.microsoft.com/microsoft-365",
            "https://learn.microsoft.com/office",
            "https://learn.microsoft.com/powershell",
            "https://learn.microsoft.com/sql",
            "https://learn.microsoft.com/surface",
            "https://dotnetfoundation.org",
            "https://learn.microsoft.com/visualstudio",
            "https://learn.microsoft.com/windows"
        };
        
        private static void Calculate()
        {
            // <PerformGameCalculation>
            static DamageResult CalculateDamageDone()
            {
                return new DamageResult()
                {
                    // Code omitted:
                    //
                    // Does an expensive calculation and returns
                    // the result of that calculation.
                };
            }

            s_calculateButton.Clicked += async (o, e) =>
            {
                // This line will yield control to the UI while CalculateDamageDone()
                // performs its work. The UI thread is free to perform other work.
                var damageResult = await Task.Run(() => CalculateDamageDone());
                DisplayDamage(damageResult);
            };
            // </PerformGameCalculation>
        }
        
        private static void DisplayDamage(DamageResult damage)
        {
            Console.WriteLine(damage.Damage);
        }
        
        private static void Download(string URL)
        {
            // <UnblockingDownload>
            s_downloadButton.Clicked += async (o, e) =>
            {
                // This line will yield control to the UI as the request
                // from the web service is happening.
                //
                // The UI thread is now free to perform other work.
                var stringData = await s_httpClient.GetStringAsync(URL);
                DoSomethingWithData(stringData);
            };
            // </UnblockingDownload>
        }
        
        private static void DoSomethingWithData(object stringData)
        {
            Console.WriteLine("Displaying data: ", stringData);
        }

        // <GetUsersForDataset>
        private static async Task<User> GetUserAsync(int userId)
        {
            // Code omitted:
            //
            // Given a user Id {userId}, retrieves a User object corresponding
            // to the entry in the database with {userId} as its Id.

            return await Task.FromResult(new User() { Id = userId });
        }
        
        private static async Task<IEnumerable<User>> GetUsersAsync(IEnumerable<int> userIds)
        {
            var getUserTasks = new List<Task<User>>();
            foreach (int userId in userIds)
            {
                getUserTasks.Add(GetUserAsync(userId));
            }

            return await Task.WhenAll(getUserTasks);
        }
        // </GetUsersForDataset>

        // <GetUsersForDatasetByLINQ>
        private static async Task<User[]> GetUsersAsyncByLINQ(IEnumerable<int> userIds)
        {
            //Be cautious when mixing LINQ with asynchronous code. Because LINQ uses deferred (lazy) execution,
            //async calls won't happen immediately as they do in a foreach loop unless you force the generated sequence
            //to iterate with a call to .ToList() or .ToArray().
            //The below example uses Enumerable.ToArray to perform the query eagerly and store the results in an array.
            //That forces the code id => GetUserAsync(id) to run and start the task.
            var getUserTasks = userIds.Select(id => GetUserAsync(id)).ToArray();
            return await Task.WhenAll(getUserTasks);
        }
        // </GetUsersForDatasetByLINQ>
        
        // <ExtractDataFromNetwork>
        [HttpGet, Route("DotNetCount")]
        static public async Task<int> GetDotNetCount(string URL)
        {
            // Suspends GetDotNetCount() to allow the caller (the web server)
            // to accept another request, rather than blocking on this one.
            var html = await s_httpClient.GetStringAsync(URL);
            return Regex.Matches(html, @"\.NET").Count;
        }
        // </ExtractDataFromNetwork>

        //Breakfast Example Methods
        static async Task<Toast> MakeToastWithButterAndJamAsync(int number)
        {
            var toast = await ToastBreadAsync(number);
            ApplyButter(toast);
            ApplyJam(toast);

            return toast;
        }
        
        private static Juice PourOJ()
        {
            Console.WriteLine("Pouring orange juice");
            return new Juice();
        }

        private static void ApplyJam(Toast toast) =>
            Console.WriteLine("Putting jam on the toast");

        private static void ApplyButter(Toast toast) =>
            Console.WriteLine("Putting butter on the toast");

        private static async Task<Toast> ToastBreadAsync(int slices)
        {
            for (int slice = 0; slice < slices; slice++)
            {
                Console.WriteLine("Putting a slice of bread in the toaster");
            }
            Console.WriteLine("Start toasting...");
            await Task.Delay(3000);
            Console.WriteLine("Remove toast from toaster");

            return new Toast();
        }
        
        private static async Task<Bacon> FryBaconAsync(int slices)
        {
            Console.WriteLine($"putting {slices} slices of bacon in the pan");
            Console.WriteLine("cooking first side of bacon...");
            await Task.Delay(3000);
            for (int slice = 0; slice < slices; slice++)
            {
                Console.WriteLine("flipping a slice of bacon");
            }
            Console.WriteLine("cooking the second side of bacon...");
            await Task.Delay(3000);
            Console.WriteLine("Put bacon on plate");

            return new Bacon();
        }
        
        private static async Task<Egg> FryEggsAsync(int howMany)
        {
            Console.WriteLine("Warming the egg pan...");
            await Task.Delay(3000);
            Console.WriteLine($"cracking {howMany} eggs");
            Console.WriteLine("cooking the eggs ...");
            await Task.Delay(3000);
            Console.WriteLine("Put eggs on plate");

            return new Egg();
        }

        private static Coffee PourCoffee()
        {
            Console.WriteLine("Pouring coffee");
            return new Coffee();
        }
        
        static async Task Main(string[] args)
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
            
            //Task.GetAwaiter().GetResult() is preferred over Task.Wait and Task.Result
            //because it propagates exceptions rather than wrapping them in an AggregateException.
            //However, all these methods cause potential deadlock and thread pool starvation issues.
            //They should all be avoided in favor of async/await
            var resultAsync2 = AsyncReturnType.MethodAsync2(5);
            resultAsync2.Wait();
            resultAsync2.GetAwaiter().GetResult();

            //Async Program Code
            Console.WriteLine("Application started.");

            Console.WriteLine("Counting '.NET' phrase in websites...");
            int total = 0;
            try
            {
                foreach (string url in s_urlList)
                {
                    var result = await GetDotNetCount(url);
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
            var users = await GetUsersAsync(ids);
            foreach (User? user in users)
            {
                Console.WriteLine($"{user.Id}: isEnabled={user.IsEnabled}");
            }

            Console.WriteLine("Application ending.");
            
            
            //Breakfast Example for simulating the Async operation
            Coffee cup = PourCoffee();
            Console.WriteLine("coffee is ready");

            var eggsTask = FryEggsAsync(2);
            var baconTask = FryBaconAsync(3);
            var toastTask = MakeToastWithButterAndJamAsync(2);

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

            Juice oj = PourOJ();
            Console.WriteLine("Orange Juice is ready");
            Console.WriteLine("Breakfast is ready!");
            Console.WriteLine("This is a test comment for checkin to git via MAC");
            Console.ReadLine();
        }
    }

    public class Coffee
    {
    }

    public class Juice
    {
    }

    public class Egg
    {
    }

    public class Toast
    {
    }

    public class Bacon
    {
    }
    
}
