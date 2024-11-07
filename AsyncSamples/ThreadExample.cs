namespace AsyncSamples
{
    public class ThreadExample
    {
        public static int MyMethodSync(int count)
        {
            int result = 0;
            for (int i = 1; i < count; i++)
            {
                Thread.Sleep(500);
                Console.WriteLine("Sunc Number Print - " + i);
                result += i;
            }
            return result;
        }

        public static Task<int> MyMethodAsync(int count)
        {
            Task<int> task = new Task<int>(() =>
            {
                int result = 0;
                for (int i = 1; i < count; i++)
                {
                    Thread.Sleep(500);
                    Console.WriteLine("Async Number Print - " + i);
                    result += i;
                }
                return result;
            });
            task.Start(); //If we don't write this line there will be no result on the console
            return task;
        }
    }
}
