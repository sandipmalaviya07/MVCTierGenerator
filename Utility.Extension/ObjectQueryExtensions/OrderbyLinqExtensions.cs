using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Extension.ObjectQueryExtensions
{
   public static class OrderbyLinqExtensions
    {
        public static IQueryable OrderBySQLSyntax(this IQueryable source, string sSQLOrder)
        {
            string[] asOrder = sSQLOrder.Split(new char[] { ' ' }, 2);
            bool descending = (asOrder.Length == 2 && String.Equals(asOrder[1], "DESC"));
            return OrderByExtension(source, asOrder[0], descending);
        }

        /// Used for sorting in ascending/descending order according to the property provided.
        public static IQueryable OrderByExtension(this IQueryable source, string propertyName, bool descending)
        {
            ParameterExpression x = Expression.Parameter(source.ElementType, "x");
            LambdaExpression selector = Expression.Lambda(Expression.PropertyOrField(x, propertyName), x);
            MethodCallExpression mce = Expression.Call(typeof(Queryable), descending ? "OrderByDescending" : "OrderBy",
                new Type[] { source.ElementType, selector.Body.Type }, source.Expression, selector);
            return source.Provider.CreateQuery(mce);
        }

    }
}
