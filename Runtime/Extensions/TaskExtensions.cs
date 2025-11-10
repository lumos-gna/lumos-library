using System;
using System.Threading.Tasks;

namespace LumosLib
{
    public class TaskExtensions
    {
        public static async Task WaitUntil(Func<bool> predicate, int checkIntervalMs = 50)
        {
            while (!predicate())
            {
                await Task.Delay(checkIntervalMs);
            }
        }
    }
}