
namespace TaskManagement.BusinessService
{
    public interface IPageLinkContaining : ILinkContaining
    {
        int PageNumber { get; set; }
        int PageCount { get; set; }
    }
}