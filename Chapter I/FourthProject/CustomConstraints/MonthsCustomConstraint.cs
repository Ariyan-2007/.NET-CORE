
using System.Text.RegularExpressions;

namespace RoutingExample.CustomConstraints
{
    public class MonthsCustomConstraint : IRouteConstraint
    {
        public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (!values.ContainsKey(routeKey))
            {
                return false;
            }

            Regex regex = new Regex("^(jan|feb|mar|apr|may|jun|jul|aug|sep|oct|nov|dec)$");

            string? monthValue = Convert.ToString(values[routeKey]);

            if (!string.IsNullOrEmpty(monthValue) && regex.IsMatch(monthValue))
            {
                return true;
            }

            return false;
        }
    }
}