using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TaskManagement.BusinessService;
using TaskManagement.Common;

namespace TaskManagement.Web.Api.Controllers.V1
{
    [ApiVersion1RoutePrefix("tasks")]
    [Authorize(Roles = Constants.RoleNames.JuniorWorker)]
    public class TasksController : ApiController
    {
        private readonly ITaskMaintenanceProcessor _taskMaintProcessor;

        private readonly IPagedDataRequestFactory _pagedDataRequestFactory;
        private readonly IAllTasksInquiryProcessor _allTasksInquiryProcessor;

        public TasksController(ITaskMaintenanceProcessor taskMaintProcessor,
                              IPagedDataRequestFactory pagedDataRequestFactory,
                              IAllTasksInquiryProcessor allTasksInquiryProcessor)
        {
            _taskMaintProcessor = taskMaintProcessor;
            _pagedDataRequestFactory = pagedDataRequestFactory;
            _allTasksInquiryProcessor = allTasksInquiryProcessor;
        }

        [Route("", Name = "AddTaskRoute")]
        [HttpPost]
        [Authorize(Roles = Constants.RoleNames.Manager)]
        [ValidateModel]
        public IHttpActionResult AddTask(HttpRequestMessage requestMessage, NewTask newTask)
        {
            var task = _taskMaintProcessor.AddTask(newTask);
            var result = new TaskCreatedActionResult(requestMessage, task);
            return result;
        }

        [Route("{id:long}", Name = "GetTaskRoute")]
        public Task GetTask(long id)
        {
            var task = _allTasksInquiryProcessor.GetTask(id);
            return task;
        }

        [Route("", Name = "GetTasksRoute")]
        public PagedDataInquiryResponse<Task> GetTasks(HttpRequestMessage requestMessage)
        {
            var request = _pagedDataRequestFactory.Create(requestMessage.RequestUri);
            var tasks = _allTasksInquiryProcessor.GetTasks(request);
            return tasks;
        }

        [Route("{id:long}", Name = "UpdateTaskRoute")]
        [HttpPut]
        [HttpPatch]
        [Authorize(Roles = Constants.RoleNames.SeniorWorker)]
        [ValidateTaskUpdateRequest]
        public Task UpdateTask(long id, [FromBody] object updatedTask)
        {
            var task = _taskMaintProcessor.UpdateTask(id, updatedTask);
            return task;
        }
    }
}