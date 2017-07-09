
using TaskManagement.Common;

namespace TaskManagement.BusinessService
{
    public interface IAllTasksInquiryProcessor
    {
        User GetUser(string name);

        Task GetTask(long taskId);

        PagedDataInquiryResponse<Task> GetTasks(PagedDataRequest requestInfo);
    }
}