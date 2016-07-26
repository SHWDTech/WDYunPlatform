using System;
using System.Linq;
using System.Linq.Expressions;

namespace SHWDTech.Platform.Utility.ExtensionMethod
{
    public static class QueryableExtensioins
    {
        public static IQueryable<T> AddEqual<T>(this IQueryable<T> queryable, Expression<Func<T, bool>> exp, object value)
        {
            if (value is Guid && (Guid) value == Guid.Empty)
            {
                return queryable;
            }

            if (value is string && string.IsNullOrWhiteSpace(value.ToString()))
            {
                return queryable;
            }

            return queryable.Where(exp);
        }
    }
}
