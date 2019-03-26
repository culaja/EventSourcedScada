using Common;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public static class TaskResultExtensions
    {
        public static IActionResult ToActionResult(this Result result) =>
            result.IsSuccess ? new JsonResult("OK") : BadRequestWith(result.Error);

        public static IActionResult ToActionResult<T>(this Result<T> result) =>
            result.IsSuccess ? new JsonResult(result.Value) : BadRequestWith(result.Error);

        private static JsonResult BadRequestWith(string error) => new JsonResult(error) {StatusCode = 400};
    }
}