using System;

namespace TaskManagement.Common
{
    public interface IPagedDataRequestFactory
    {
        //amit
        PagedDataRequest Create(Uri requestUri);
    }
}