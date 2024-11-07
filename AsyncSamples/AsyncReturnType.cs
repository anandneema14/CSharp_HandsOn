using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncSamples
{
    public class AsyncReturnType
    {
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
    }
}
