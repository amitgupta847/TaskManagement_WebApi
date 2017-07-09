using System;
using System.Collections.Generic;
using System.Linq;
using TaskManagement.BusinessService;
using TaskManagement.Common;
using TaskManagement.Data.SqlServer;
using PagedTaskDataInquiryResponse = TaskManagement.BusinessService.PagedDataInquiryResponse<TaskManagement.BusinessService.Task>;

namespace TaskManagement.BusinessServices.Processors
{
    public class AllTasksInquiryProcessor : IAllTasksInquiryProcessor
    {
        public const string QueryStringFormat = "pagenumber={0}&pagesize={1}";

        private readonly IAutoMapper _autoMapper;
        private readonly ICommonLinkService _commonLinkService;
        private readonly ITaskDAS _queryProcessor;
        private readonly ITaskLinkService _taskLinkService;

        public AllTasksInquiryProcessor(ITaskDAS queryProcessor, IAutoMapper autoMapper,
            ITaskLinkService taskLinkService, ICommonLinkService commonLinkService)
        {
            _queryProcessor = queryProcessor;
            _autoMapper = autoMapper;
            _taskLinkService = taskLinkService;
            _commonLinkService = commonLinkService;
        }

        public Task GetTask(long taskId)
        {
            var taskEntity = _queryProcessor.GetTask(taskId);
            if (taskEntity == null)
            {
                throw new RootObjectNotFoundException("Task not found");
            }

            var task = _autoMapper.Map<Task>(taskEntity);

            // _taskLinkService.AddLinks(task);

            return task;
        }

        public User GetUser(string name)
        {
            var userEntity= _queryProcessor.GetUser(name);
            var user = _autoMapper.Map<User>(userEntity);
            return user;
        }


        public PagedTaskDataInquiryResponse GetTasks(PagedDataRequest requestInfo)
        {
            var queryResult = _queryProcessor.GetTasks(requestInfo);

            var tasks = GetTasks(queryResult.QueriedItems).ToList();

            var inquiryResponse = new PagedTaskDataInquiryResponse
            {
                Items = tasks,
                PageCount = queryResult.TotalPageCount,
                PageNumber = requestInfo.PageNumber,
                PageSize = requestInfo.PageSize
            };

            if (!requestInfo.ExcludeLinks)
            {
                AddLinksToInquiryResponse(inquiryResponse);
            }

            return inquiryResponse;
        }

        public virtual void AddLinksToInquiryResponse(PagedTaskDataInquiryResponse inquiryResponse)
        {
            inquiryResponse.AddLink(_taskLinkService.GetAllTasksLink());

            _commonLinkService.AddPageLinks(inquiryResponse, GetCurrentPageQueryString(inquiryResponse),
                GetPreviousPageQueryString(inquiryResponse),
                GetNextPageQueryString(inquiryResponse));
        }

        public virtual IEnumerable<Task> GetTasks(IEnumerable<TaskManagement.Data.SqlServer.DataEntities.Task> taskEntities)
        {
            var tasks = taskEntities.Select(x => _autoMapper.Map<Task>(x)).ToList();

            tasks.ForEach(x =>
            {
                _taskLinkService.AddSelfLink(x);
                _taskLinkService.AddLinksToChildObjects(x);
            });

            return tasks;
        }

        public virtual string GetCurrentPageQueryString(PagedTaskDataInquiryResponse inquiryResponse)
        {
            return
                string.Format(QueryStringFormat,
                    inquiryResponse.PageNumber,
                    inquiryResponse.PageSize);
        }

        public virtual string GetPreviousPageQueryString(PagedTaskDataInquiryResponse inquiryResponse)
        {
            return
                string.Format(QueryStringFormat,
                    inquiryResponse.PageNumber - 1,
                    inquiryResponse.PageSize);
        }

        public virtual string GetNextPageQueryString(PagedTaskDataInquiryResponse inquiryResponse)
        {
            return string.Format(QueryStringFormat,
                inquiryResponse.PageNumber + 1,
                inquiryResponse.PageSize);
        }

      
    }
}