﻿using System.Collections.Generic;

namespace TaskManagement.Common
{
    public class QueryResult<T>
    {
        public QueryResult(IEnumerable<T> queriedItems, int totalItemCount, int pageSize)
        {
            PageSize = pageSize;
            TotalItemCount = totalItemCount;
            QueriedItems = queriedItems ?? new List<T>();
        }

        /// <summary>
        ///     The total number of items, unrestricted by paging logic.
        /// </summary>
        public int TotalItemCount { get; private set; }

        public int TotalPageCount
        {
            get { return ResultsPagingUtility.CalculatePageCount(TotalItemCount, PageSize); }
        }

        public IEnumerable<T> QueriedItems { get; private set; }

        public int PageSize { get; private set; }
    }
}