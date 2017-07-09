
namespace TaskManagement.BusinessService
{
    public interface ITaskLinkService
    {
        void AddLinks(Task task);
        Link GetAllTasksLink();
        void AddSelfLink(Task task);
        void AddLinksToChildObjects(Task task);
        Link GetSelfLink(long taskId);
    }
}