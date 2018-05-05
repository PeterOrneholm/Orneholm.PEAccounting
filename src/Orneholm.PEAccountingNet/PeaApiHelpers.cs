using System;
using System.Collections.Generic;
using System.Linq;

namespace Orneholm.PEAccountingNet
{
    internal static class PeaApiHelpers
    {
        public static IEnumerable<TItem> TransformListResult<TResult, TItem>(TResult result, Func<TResult, IEnumerable<TItem>> projection)
        {
            if (result == null)
            {
                return Enumerable.Empty<TItem>();
            }

            var items = projection(result);
            if (items == null)
            {
                return Enumerable.Empty<TItem>();
            }

            return items;
        }
    }
}