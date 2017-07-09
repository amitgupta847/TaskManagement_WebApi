using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManagement.BusinessService 
{
    public interface ITaskMaintenanceProcessor
    {
        Task AddTask(NewTask newTask);

        Task StartTask(long taskId);

        Task CompleteTask(long taskId);

        Task ReactivateTask(long taskId);

        Task UpdateTask(long taskId, object taskFragment);

        Task ReplaceTaskUsers(long taskId, IEnumerable<long> userIds);
        Task DeleteTaskUsers(long taskId);
        Task AddTaskUser(long taskId, long userId);
        Task DeleteTaskUser(long taskId, long userId);
    }
}