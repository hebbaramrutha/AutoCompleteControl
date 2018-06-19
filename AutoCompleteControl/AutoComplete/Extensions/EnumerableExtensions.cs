using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCompleteControl.AutoComplete.Extensions
{
   public static class EnumerableExtensions
    {
        public static bool IsCollectionValid<T>(this IEnumerable<T> source)
        {
            return source != null && source.Any();
        }

        public static IEnumerable<T> Each<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (source != null)
            {
                foreach (var item in source)
                {
                    action(item);
                }
            }
            return source;
        }
    }
}
