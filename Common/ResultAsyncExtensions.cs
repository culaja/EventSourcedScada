using System;
using System.Threading.Tasks;

namespace Common
{
    public static class ResultAsyncExtensions
    {
        public static async Task<Result<T>> OnSuccess<T>(this Task<Result> taskResult, Func<Task<T>> func)
        {
            var result = await taskResult;
            if (result.IsFailure)
                return Result.Fail<T>(result.Error);

            return Result.Ok(await func());
        }
    }
}