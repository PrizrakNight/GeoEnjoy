using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace GeoEnjoy.WebApi.Extensions
{
    public static class ResultExtensions
    {
        public static IActionResult ToActionResult<TValue>(this Result<TValue> result)
        {
            throw new NotImplementedException();
        }
    }
}
