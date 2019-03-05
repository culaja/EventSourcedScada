using System.Threading.Tasks;
using Common;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public static class TaskResultExtensions
    {
        public static async Task<IActionResult> ToActionResultAsync(this Task<Result> taskResult)
        {
            var result = await taskResult;
            return result.IsSuccess ? new JsonResult("OK") : BadRequestWith(result.Error);
        }

        private static JsonResult BadRequestWith(string error) => new JsonResult(error) {StatusCode = 400};
    }
}