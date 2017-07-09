using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Common
{
    public class PagedDataRequest
    {
        public PagedDataRequest(int pageNumber, int pageSize)
        {
            PageNumber =  pageNumber;
            PageSize = pageSize;
        }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public bool ExcludeLinks { get; set; }
    }
}
