using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskChains
{
    class Program
    {
        private static Task<int> _CurrentTask;

        static void Main(string[] args)
        {
            var task1 = DoWorkAsync(1);
            var task2 = DoWorkAsync(2);
            var task3 = DoWorkAsync(3);
            var task4 = DoWorkAsync(4);

            Task.WaitAll(task1, task2, task3, task4);

            Console.WriteLine("All Tasks have finished");
            Console.ReadKey();

        }

        private static Task<int> DoWorkAsync(int taskId)
        {
            return _CurrentTask = _CurrentTask.ChainTask(() => GenerateNumber(taskId));
        }

        private static Task<int> GenerateNumber(int taskId)
        {
            return Task.Run(() =>
            {
                Console.WriteLine($"Critical Task {taskId} started");
                Thread.Sleep(2000);
                Console.WriteLine($"Critical Task {taskId} finished");

                return taskId;
            });
        }
    }
}
