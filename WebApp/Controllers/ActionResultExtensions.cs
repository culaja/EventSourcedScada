using System;
using Microsoft.AspNetCore.Mvc;
using static System.Net.HttpStatusCode;

namespace WebApp.Controllers
{
    public static class ActionResultExtensions
    {
        public static IActionResult CombineWith(this IActionResult ar1, IActionResult ar2)
        {
            var jr1 = (JsonResult) ar1;
            var jr2 = (JsonResult) ar2;

            if (jr1.StatusCode == (int) OK && jr2.StatusCode == (int) OK) return new JsonResult("OK");
            if (jr1.StatusCode != (int) OK) return new JsonResult(jr1.Value) {StatusCode = jr1.StatusCode};
            if (jr2.StatusCode != (int) OK) return new JsonResult(jr2.Value) {StatusCode = jr2.StatusCode};

            throw new InvalidOperationException("This case is not possible");
        }
    }
}