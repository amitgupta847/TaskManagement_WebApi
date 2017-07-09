using System.Collections.Generic;
using System.Web.Http;
using TaskManagement.BusinessService;

namespace TaskManagement.Web.Api.Controllers.V1
{
    [ApiVersion1RoutePrefix("tasks")]
    [Authorize(Roles = Constants.RoleNames.SeniorWorker)]
    public class TaskUsersController : ApiController
    {
       private readonly ITaskMaintenanceProcessor _taskMaintenanceProcessor;
        private readonly IAllTasksInquiryProcessor _taskInquiryProcessor;


        public TaskUsersController( ITaskMaintenanceProcessor taskMaintProcessor, 
                                    IAllTasksInquiryProcessor taskInquiryProcessor)
        {
            _taskMaintenanceProcessor = taskMaintProcessor;
            _taskInquiryProcessor = taskInquiryProcessor;
        }

        //[Route("{taskId:long}/users", Name = "GetTaskUsersRoute")]
        //public TaskUsersInquiryResponse GetTaskUsers(long taskId)
        //{
        //    var users = _taskInquiryProcessor.GetTaskUsers(taskId);
        //    return users;
        //}

        [Route("{taskId:long}/users", Name = "ReplaceTaskUsersRoute")]
        [HttpPut]
        public Task ReplaceTaskUsers(long taskId, [FromBody] IEnumerable<long> userIds)
        {
            var task = _taskMaintenanceProcessor.ReplaceTaskUsers(taskId, userIds);
            return task;
        }

        [Route("{taskId:long}/users", Name = "DeleteTaskUsersRoute")]
        [HttpDelete]
        public Task DeleteTaskUsers(long taskId)
        {
            var task = _taskMaintenanceProcessor.DeleteTaskUsers(taskId);
            return task;
        }

        [Route("{taskId:long}/users/{userId:long}", Name = "AddTaskUserRoute")]
        [HttpPut]
        public Task AddTaskUser(long taskId, long userId)
        {
            var task = _taskMaintenanceProcessor.AddTaskUser(taskId, userId);
            return task;
        }

        [Route("{taskId:long}/users/{userId:long}", Name = "DeleteTaskUserRoute")]
        [HttpDelete]
        public Task DeleteTaskUser(long taskId, long userId)
        {
            var task = _taskMaintenanceProcessor.DeleteTaskUser(taskId, userId);
            return task;
        }
    }
}