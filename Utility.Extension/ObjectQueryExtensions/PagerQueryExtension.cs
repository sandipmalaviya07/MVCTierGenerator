using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Utility.Extension.Paging;

namespace Utility.Extension.ObjectQueryExtensions
{
    public static class PagerQueryExtension
    {
        public static PagerList<T> ToPagerListOrderBy<T>(this IQueryable<T> query, int PageNumber, int PageRows, string orderBy)
        {
            if (PageNumber < 1)
                PageNumber = 1;
            var itemsToSkip = (PageNumber - 1) * PageRows;
            var totalRows = query.Count();
            var pagerList = query.OrderBy(orderBy).Skip(itemsToSkip).Take(PageRows).AsEnumerable();
            return new PagerList<T>(pagerList, PageNumber, PageRows, totalRows, orderBy);
        }

        public static PagerList<T> ToPagerList<T>(this IQueryable<T> query, int PageNumber, int PageRows)
        {
            if (PageNumber < 1)
                PageNumber = 1;
            var itemsToSkip = (PageNumber - 1) * PageRows;
            var totalRows = query.Count();
            var pagerList = query.Skip(itemsToSkip).Take(PageRows).AsEnumerable();
            return new PagerList<T>(pagerList, PageNumber, PageRows, totalRows);
        }

        public static PagerList<T> ToPagerListOrderByExpression<T, TProperty>(this IQueryable<T> query, int PageNumber, int PageRows, Expression<Func<T, TProperty>> orderBy)
        {
            if (PageNumber < 1)
                PageNumber = 1;
            var itemsToSkip = (PageNumber - 1) * PageRows;
            var totalRows = query.Count();
            var pagerList = query.OrderBy(orderBy).Skip(itemsToSkip).Take(PageRows).AsEnumerable();
            return new PagerList<T>(pagerList, PageNumber, PageRows, totalRows, orderBy.Name);
        }
        public static PagerList<T> ToPagerListOrderByDesendingExpression<T, TProperty>(this IQueryable<T> query, int PageNumber, int PageRows, Expression<Func<T, TProperty>> orderBy)
        {
            if (PageNumber < 1)
                PageNumber = 1;
            var itemsToSkip = (PageNumber - 1) * PageRows;
            var totalRows = query.Count();
            var pagerList = query.OrderByDescending(orderBy).Skip(itemsToSkip).Take(PageRows).AsEnumerable();
            return new PagerList<T>(pagerList, PageNumber, PageRows, totalRows, orderBy.Name);
        }
        public static PagerList<T> ToPagerListOffSet<T>(this IQueryable<T> query, int StartRow, int PageRows)
        {
            int PageNumber=(StartRow/PageRows)+1;
            var itemsToSkip = (PageNumber - 1) * PageRows;
            var totalRows = query.Count();
            var pagerList = query.Skip(itemsToSkip).Take(PageRows).AsEnumerable();
            return new PagerList<T>(pagerList, PageNumber, PageRows, totalRows);
        }

    }
}
