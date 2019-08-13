using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncBestPractices
{
    class Program
    {
        /*
         * ValueTask is a new feature that stores data on the stack and not on the hip as the standard Task
         * 
         * If the is no "await" there is no other "Thread"
         * 
         *  Not every Task is a Thread
         */

        // Class just to make a calls to
        private static Task<string> CallString()
        {
            return Task.FromResult("Hello string from CallString Method");
        }


        static void Main(string[] args)
        {
            // This helps us to recieve an exceptions and other data from the Task, much better then just "Wait()"
            AwaitableTask().GetAwaiter().GetResult();

            // This Task To test all others with await key word
            MainTestTask().GetAwaiter().GetResult();
            Print();

            Console.WriteLine("This is the end");
        }

        // async void that is not awaited
        private static async void Print()
        {
            // The thing is "Print()" wil never return a value to Thread 1 because it is not awaited
            // Be careful with async void
            // Much better to use Tasks
            Thread.Sleep(5000);
            Console.WriteLine("Some data to write");
        }

        // Task to call async tasks
        private static async Task MainTestTask()
        {
            await DontWaitForThreadOne();
        }

        // Task that awaited
        private static async Task AwaitableTask()
        {
            // This taks is waited from sync method wich
            Console.WriteLine("This is Awaitable task");
        }

        // Task where "await" does not callback on the main thread
        private static async Task DontWaitForThreadOne()
        {
            // In this excample our await key word does not wait for Thread 1 to be free and just continues to run the function
            var str = await CallString().ConfigureAwait(false); // This just continues on some free Thread

            Thread.Sleep(2000);
            Console.WriteLine(str + " some seconds passed");

        }
    }
}