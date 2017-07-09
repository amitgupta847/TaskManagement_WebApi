
namespace TaskManagement.BusinessService
{
    public interface IUserLinkService
    {
        void AddLinks(User user);
        Link GetAllUsersLink();
        void AddSelfLink(User user);
    }
}