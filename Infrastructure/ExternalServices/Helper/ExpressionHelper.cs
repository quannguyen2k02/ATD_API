using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ExternalServices.Helper
{
    public static class ExpressionHelper
    {
        public static Expression<Func<T, bool>> GreaterThan<T, TKey>(Expression<Func<T, TKey>> property, TKey value)
        {
            var param = property.Parameters[0];
            var body = Expression.GreaterThan(property.Body, Expression.Constant(value, typeof(TKey)));
            return Expression.Lambda<Func<T, bool>>(body, param);
        }

        public static Expression<Func<T, bool>> LessThan<T, TKey>(Expression<Func<T, TKey>> property, TKey value)
        {
            var param = property.Parameters[0];
            var body = Expression.LessThan(property.Body, Expression.Constant(value, typeof(TKey)));
            return Expression.Lambda<Func<T, bool>>(body, param);
        }
    }
}
