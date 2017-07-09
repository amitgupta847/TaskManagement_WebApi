using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManagement.Common;
using TaskManagement.Data.SqlServer.DataEntities;
using PropertyValueMapType = System.Collections.Generic.Dictionary<string, object>;


namespace TaskManagement.Data.SqlServer
{
   public interface ITaskDAS
    {
        User GetUser(string name);
        void AddTask(Task task);
        QueryResult<Task> GetTasks(PagedDataRequest requestInfo);
        Task GetTask(long taskId);
        Task GetFullyLoadedTask(long taskId);
        Task GetUpdatedTask(long taskId, PropertyValueMapType updatedPropertyValueMap);
        Task ReplaceTaskUsers(long taskId, IEnumerable<long> userIds);
        Task DeleteTaskUsers(long taskId);
        Task AddTaskUser(long taskId, long userId);
        Task DeleteTaskUser(long taskId, long userId);
        void UpdateTaskStatus(Task taskToUpdate, string statusName);
    }
}
