using System.Web.Http;
using TaskManagement.BusinessService;


namespace TaskManagement.Web.Api.Controllers.V1
{
    [ApiVersion1RoutePrefix("")]
    [Authorize(Roles = Constants.RoleNames.SeniorWorker)]
    public class TaskWorkflowController : ApiController
    {
        private readonly ITaskMaintenanceProcessor _taskWorkflowProcessor;


        public TaskWorkflowController(ITaskMaintenanceProcessor taskWorkflowProcessor)
        {
            _taskWorkflowProcessor = taskWorkflowProcessor;
        }

        [HttpPost]
        [Route("tasks/{taskId:long}/activations", Name = "StartTaskRoute")]
        public Task StartTask(long taskId)
        {
            var task = _taskWorkflowProcessor.StartTask(taskId);
            return task;
        }

        [HttpPost]
        [Route("tasks/{taskId:long}/completions", Name = "CompleteTaskRoute")]
        public Task CompleteTask(long taskId)
        {
            var task = _taskWorkflowProcessor.CompleteTask(taskId);
            return task;
        }

        [HttpPost]
        [UserAudit]
        [Route("tasks/{taskId:long}/reactivations", Name = "ReactivateTaskRoute")]
        public Task ReactivateTask(long taskId)
        {
            var task = _taskWorkflowProcessor.ReactivateTask(taskId);
            return task;
        }
    }
}