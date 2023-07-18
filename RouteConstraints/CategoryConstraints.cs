using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MyWebSite.Services;
using System.Linq;

namespace MyWebSite.RouteConstraints
{
    public class CategoryConstraints : IRouteConstraint
    {
        JsonFileInfoService JsonInfo { get; set; }
        public CategoryConstraints(JsonFileInfoService jsonInfo) {
            JsonInfo = jsonInfo;
        }


        bool IRouteConstraint.Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            var categories = JsonInfo.GetCategories();

            if (categories == null) return false;

            if (categories.Contains(values[routeKey]?.ToString().ToLowerInvariant())) { }
            else
                values[routeKey] = "popular";

            return true;
        }
    }
}
