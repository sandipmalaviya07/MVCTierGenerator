using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Extension.Paging
{
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

}
