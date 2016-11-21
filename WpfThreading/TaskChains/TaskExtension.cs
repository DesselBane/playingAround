using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskChains
{
    public static class TaskExtension
    {
        public static Task<TResult> ChainTask <TResult>(this Task condition, Func<Task<TResult>>  newTaskGenerator)
        {
            return Task.Run(async () =>
            {
                if (condition != null)
                    await condition;

                return await newTaskGenerator();
            });
        }
    }
}
