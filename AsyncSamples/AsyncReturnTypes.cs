namespace AsyncSamples;

public class AsyncReturnTypes
{
    /// <summary>
    /// Task Return Type:
    /// Async methods that don't contain a return statement
    /// or that contain a return statement that doesn't return an operand usually have a return type of Task
    ///
    /// In the following example, the WaitAsync method doesn't contain a return statement, so the method returns a Task object.
    /// Returning a Task enables WaitAsync to be awaited.
    /// The Task type doesn't include a Result property because it has no return value.
    /// </summary>
    public static async Task DisplayCurrentInfoAsync()
    {
        await WaitAsync();

        Console.WriteLine($"Today is {DateTime.Now:D}");
        Console.WriteLine($"The current time is {DateTime.Now.TimeOfDay:t}");
    }

    static async Task WaitAsync()
    {
        await Task.Delay(2000);

        Console.WriteLine("Sorry for the delay...\n");
    }
    
    public static Task MethodAsync1(int count)
    {
        Task task = new Task(() =>
        {
            for (int i = 1; i <= count; i++)
            {
                Thread.Sleep(200);
                Console.WriteLine("Async Number: " + i);
            }
        });
        task.Start();
        return task;
    }
// Example output:
//    Sorry for the delay...
//
// Today is Monday, August 17, 2020
// The current time is 12:59:24.2183304
// The current temperature is 76 degrees.

    /// <summary>
    /// Task<TResult> Return Type:
    /// The Task<TResult> return type is used for an async method that contains a return statement in which the operand is TResult.
    ///
    /// In the following example, the GetLeisureHoursAsync method contains a return statement that returns an integer.
    /// The method declaration must specify a return type of Task<int>.
    /// The FromResult async method is a placeholder for an operation that returns a DayOfWeek.
    /// </summary>
    public static async Task ShowTodaysInfoAsync()
    {
        string message =
            $"Today is {DateTime.Today:D}\n" +
            "Today's hours of leisure: " +
            $"{await GetLeisureHoursAsync()}";

        Console.WriteLine(message);
    }

    static async Task<int> GetLeisureHoursAsync()
    {
        DayOfWeek today = await Task.FromResult(DateTime.Now.DayOfWeek);

        int leisureHours =
            today is DayOfWeek.Saturday || today is DayOfWeek.Sunday
                ? 16 : 5;

        return leisureHours;
    }
    
    public static Task<int> MethodAsync2(int count)
    {
        int result = 0;
        Task<int> task = new Task<int>(() =>
        {
            for (int i = 1; i <= count; i++)
            {
                Thread.Sleep(200);
                Console.WriteLine("Async Number: " + i);
                result += i;
            }
            return result;
        });
        task.Start();
        return task;
    }
// Example output:
//    Today is Wednesday, May 24, 2017
//    Today's hours of leisure: 5

    /// <summary>
    /// Async streams with IAsyncEnumerable<T>
    ///
    /// An async method might return an async stream, represented by IAsyncEnumerable<T>.
    /// An async stream provides a way to enumerate items read from a stream
    /// when elements are generated in chunks with repeated asynchronous calls.
    ///
    /// The preceding example reads lines from a string asynchronously.
    /// Once each line is read, the code enumerates each word in the string.
    /// Callers would enumerate each word using the await foreach statement.
    /// The method awaits when it needs to asynchronously read the next line from the source string.
    /// </summary>
    /// <returns></returns>
    public static async IAsyncEnumerable<string> ReadWordsFromStreamAsync()
    {
        string data =
            @"This is a line of text.
              Here is the second line of text.
              And there is one more for good measure.
              Wait, that was the penultimate line.";

        using var readStream = new StringReader(data);

        string? line = await readStream.ReadLineAsync();
        while (line != null)
        {
            foreach (string word in line.Split(' ', StringSplitOptions.RemoveEmptyEntries))
            {
                yield return word;
            }

            line = await readStream.ReadLineAsync();
        }
    }
}