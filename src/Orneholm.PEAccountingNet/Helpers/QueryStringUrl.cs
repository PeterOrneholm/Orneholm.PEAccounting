using System;
using System.Collections.Generic;
using System.Linq;

namespace Orneholm.PEAccountingNet.Helpers
{
    internal static class QueryStringUrl
    {
        internal static string GetUrl(string baseUrl, Dictionary<string, string> filter)
        {
            if (!filter.Any())
            {
                return baseUrl;
            }

            var queryString = string.Join("&", filter.Select(x => $"{x.Key}={Uri.EscapeDataString(x.Value)}"));
            return $"{baseUrl}?{queryString}";
        }
    }
}