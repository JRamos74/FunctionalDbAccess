using System;
using System.Threading.Tasks;

namespace FnDbAccess.Extensions
{
    /// <summary>
    /// Provides extension methods for the using statement
    /// </summary>
    public static class FnExtension
    {
        public static TResult Using<TDisp, TResult>(TDisp disposable, Func<TDisp, TResult> fn) where TDisp : IDisposable
        {
            using (var disp = disposable) return fn(disp);
        }

        public static async Task<TResult> UsingAsync<TDisp, TResult>(TDisp disposable, Func<TDisp, Task<TResult>> fn) where TDisp : IDisposable
        {
            using (var disp = disposable) return await fn(disp);
        }
    }
}
