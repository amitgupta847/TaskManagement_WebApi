using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using TaskManagement.BusinessService;
using TaskManagement.Common;
using TaskManagement.Data.SqlServer;
using PropertyValueMapType = System.Collections.Generic.Dictionary<string, object>;

namespace TaskManagement.BusinessServices.Processors
{
    public class TaskMaintenanceProcessor : ITaskMaintenanceProcessor
    {

        private readonly IAutoMapper _autoMapper;
        private readonly ITaskDAS _queryProcessor;
        private readonly ITaskLinkService _taskLinkService;
        private readonly IDateTime _dateTime;
        private readonly IUpdateablePropertyDetector _updateablePropertyDetector;

        public TaskMaintenanceProcessor(IAllTasksInquiryProcessor taskInquireProcessor,
                                        ITaskDAS queryProcessor,
                                        IAutoMapper autoMapper,
                                        ITaskLinkService taskLinkService,
                                        IDateTime dateTime,
            IUpdateablePropertyDetector updateablePropertyDetector)
        {
            _queryProcessor = queryProcessor;
            _autoMapper = autoMapper;
            _taskLinkService = taskLinkService;
            _dateTime = dateTime;
            _updateablePropertyDetector = updateablePropertyDetector;
        }

        public Task AddTask(NewTask newTask)
        {
            var taskEntity = _autoMapper.Map<TaskManagement.Data.SqlServer.DataEntities.Task>(newTask);

            _queryProcessor.AddTask(taskEntity);

            var task = _autoMapper.Map<Task>(taskEntity);

            _taskLinkService.AddLinks(task);

            return task;
        }

        public Task AddTaskUser(long taskId, long userId)
        {
            var taskEntity = _queryProcessor.AddTaskUser(taskId, userId);
            return CreateTaskResponse(taskEntity);
        }

        public Task CompleteTask(long taskId)
        {
            var taskEntity = _queryProcessor.GetFullyLoadedTask(taskId);
            if (taskEntity == null)
            {
                throw new RootObjectNotFoundException("Task not found");
            }

            // Simulate some workflow logic...
            if (taskEntity.Status.Name != "In Progress")
            {
                throw new BusinessRuleViolationException("Incorrect task status. Expected status of 'In Progress'.");
            }

            taskEntity.CompletedDate = _dateTime.UtcNow;
            _queryProcessor.UpdateTaskStatus(taskEntity, "Completed");

            var task = _autoMapper.Map<Task>(taskEntity);

            _taskLinkService.AddLinks(task);

            return task;
        }

        public Task DeleteTaskUser(long taskId, long userId)
        {
            var taskEntity = _queryProcessor.DeleteTaskUser(taskId, userId);
            return CreateTaskResponse(taskEntity);
        }

        public Task DeleteTaskUsers(long taskId)
        {
            var taskEntity = _queryProcessor.DeleteTaskUsers(taskId);
            return CreateTaskResponse(taskEntity);
        }

        public Task ReactivateTask(long taskId)
        {
            var taskEntity = _queryProcessor.GetFullyLoadedTask(taskId);
            if (taskEntity == null)
            {
                throw new RootObjectNotFoundException("Task not found");
            }

            // Simulate some workflow logic...
            if (taskEntity.Status.Name != "Completed")
            {
                throw new BusinessRuleViolationException("Incorrect task status. Expected status of 'Completed'.");
            }

            taskEntity.CompletedDate = null;
            _queryProcessor.UpdateTaskStatus(taskEntity, "In Progress");

            var task = _autoMapper.Map<Task>(taskEntity);

            _taskLinkService.AddLinks(task);

            return task;
        }

        public Task ReplaceTaskUsers(long taskId, IEnumerable<long> userIds)
        {
            var taskEntity = _queryProcessor.ReplaceTaskUsers(taskId, userIds);
            return CreateTaskResponse(taskEntity);
        }

        private Task CreateTaskResponse(TaskManagement.Data.SqlServer.DataEntities.Task taskEntity)
        {
            var task = _autoMapper.Map<Task>(taskEntity);
            _taskLinkService.AddLinks(task);
            return task;
        }

        public Task StartTask(long taskId)
        {
            var taskEntity = _queryProcessor.GetFullyLoadedTask(taskId);
            if (taskEntity == null)
            {
                throw new RootObjectNotFoundException("Task not found");
            }

            // Simulate some workflow logic...
            if (taskEntity.Status.Name != "Not Started")
            {
                throw new BusinessRuleViolationException("Incorrect task status. Expected status of 'Not Started'.");
            }

            taskEntity.StartDate = _dateTime.UtcNow;
            _queryProcessor.UpdateTaskStatus(taskEntity, "In Progress");

            var task = _autoMapper.Map<Task>(taskEntity);

            _taskLinkService.AddLinks(task);

            return task;
        }

        public Task UpdateTask(long taskId, object taskFragment)
        {
            var taskFragmentAsJObject = (JObject)taskFragment;
            var taskContainingUpdateData = taskFragmentAsJObject.ToObject<Task>();

            var updatedPropertyValueMap = GetPropertyValueMap(taskFragmentAsJObject, taskContainingUpdateData);

            var updatedTaskEntity = _queryProcessor.GetUpdatedTask(taskId, updatedPropertyValueMap);

            var task = _autoMapper.Map<Task>(updatedTaskEntity);

            _taskLinkService.AddLinks(task);

            return task;
        }

        private PropertyValueMapType GetPropertyValueMap(JObject taskFragment, Task taskContainingUpdateData)
        {
            var namesOfModifiedProperties = _updateablePropertyDetector.GetNamesOfPropertiesToUpdate<Task>(taskFragment).ToList();

            var propertyInfos = typeof(Task).GetProperties();
            var updatedPropertyValueMap = new PropertyValueMapType();
            foreach (var propertyName in namesOfModifiedProperties)
            {
                var propertyValue = propertyInfos.Single(x => x.Name == propertyName).GetValue(taskContainingUpdateData);
                updatedPropertyValueMap.Add(propertyName, propertyValue);
            }

            return updatedPropertyValueMap;
        }
    }
}