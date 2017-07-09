using System.Net.Http;

namespace TaskManagement.BusinessService
{
    public interface ICommonLinkService
    {
        void AddPageLinks(IPageLinkContaining linkContainer,
            string currentPageQueryString,
            string previousPageQueryString,
            string nextPageQueryString);

        Link GetLink(string pathFragment, string relValue, HttpMethod httpMethod);
    }
}