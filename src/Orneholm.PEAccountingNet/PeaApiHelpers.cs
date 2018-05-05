using System;
using System.Collections.Generic;
using System.Linq;

namespace Orneholm.PEAccountingNet
{
    internal static class PeaApiHelpers
    {
        public static IEnumerable<TItem> TransformListResult<TResult, TItem>(TResult result, Func<TResult, IEnumerable<TItem>> getValue)
        {
            return TransformListResult(result, getValue, x => x);
        }

        public static IEnumerable<TItem> TransformListResult<TResult, TNativeItem, TItem>(TResult result, Func<TResult, IEnumerable<TNativeItem>> getValue, Func<TNativeItem, TItem> transformItem)
        {
            if (result == null)
            {
                return Enumerable.Empty<TItem>();
            }

            var items = getValue(result);
            if (items == null)
            {
                return Enumerable.Empty<TItem>();
            }

            return items.Select(transformItem);
        }
    }
}