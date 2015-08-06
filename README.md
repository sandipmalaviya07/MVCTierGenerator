# MVCTierGenerator
------------------- Entity Tier Generator ----------------------------------------------

Auto-generate Class of sql database table same like EDMX.

Different between EDMX and Tier generator.

1) EDMX - Lazy loading and enger loading constraint stuck to load model or data(list, collection or single object).
   Tier Generator : you can load database as you need as per you mention.

2) No need any foreign key relationship mention for get data or fetch data in Model Class.


------------------------Pager Utility -----------------------------------------------------------

1) Pager : create custom LINQ pager method which useful to get data from databse with certain mmanner.

Pager Custom Class(Global parameter for paging)
public class PagerList<T>
    {
       public int PageRows { get; private  set; }
       public IEnumerable<T> PageList { get; private set; }
       public int PageNumber { get; private set; }
       public int TotalRows { get; private set; }
       public int TotalPage { get; private set; }
       public int CurrentPage { get; private set; }
       public string SortBy { get; private set; }
       public PagerList(IEnumerable<T> pageList, int pageNumber, int pageRows, int totalRows, string sortBy = "")
        {
            PageRows = pageRows;
            TotalRows = totalRows;
            PageList = pageList;
            PageNumber = pageNumber;
            SortBy = sortBy;
            TotalPage = GeneralUtility.GetRound(GeneralUtility.GetDecimal(totalRows / PageRows));
        }
    }
    
Pager Linq Method(Nuget package : Dynamic Linq or Entityframework Express)

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
---------------------------------------------------------------------------------------------
