using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;

namespace AsyncSamples;

public class DotNETCount
{
    private static readonly Button sDownloadButton = new();
    private static readonly Button sCalculateButton = new();

    private static readonly HttpClient sHttpClient = new();
    
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

        sCalculateButton.Clicked += async (o, e) =>
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
        sDownloadButton.Clicked += async (o, e) =>
        {
            // This line will yield control to the UI as the request
            // from the web service is happening.
            //
            // The UI thread is now free to perform other work.
            var stringData = await sHttpClient.GetStringAsync(URL);
            DoSomethingWithData(stringData);
        };
        // </UnblockingDownload>
    }
    
    private static void DoSomethingWithData(object stringData)
    {
        Console.WriteLine("Displaying data: ", stringData);
    }
    
    // <ExtractDataFromNetwork>
    [HttpGet, Route("DotNetCount")]
    public static async Task<int> GetDotNetCount(string URL)
    {
        // Suspends GetDotNetCount() to allow the caller (the web server)
        // to accept another request, rather than blocking on this one.
        var html = await sHttpClient.GetStringAsync(URL);
        return Regex.Matches(html, @"\.NET").Count;
    }
    // </ExtractDataFromNetwork>
    
    // <GetUsersForDataset>
    private static async Task<User> GetUserAsync(int userId)
    {
        // Code omitted:
        //
        // Given a user Id {userId}, retrieves a User object corresponding
        // to the entry in the database with {userId} as its Id.

        return await Task.FromResult(new User() { Id = userId });
    }
        
    public static async Task<IEnumerable<User>> GetUsersAsync(IEnumerable<int> userIds)
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
}