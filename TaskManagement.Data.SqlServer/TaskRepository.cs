using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using TaskManagement.Common;
using TaskManagement.Data.SqlServer.DataEntities;

namespace TaskManagement.Data.SqlServer
{
    public class TaskRepository : ITaskDAS
    {
        private readonly IDateTime _dateTime;
        private readonly IUserSession _userSession;

        public TaskRepository(IUserSession userSession, IDateTime dateTime)
        {
            _userSession = userSession;
            _dateTime = dateTime;
        }

        public void AddTask(Task task)
        {
            task.CreatedDate = _dateTime.UtcNow;

            using (TaskManagementDbContext context = new TaskManagementDbContext())
            {
                task.Status = context.Status.Where(x => x.Name == "Not Started").SingleOrDefault();

                task.CreatedBy = context.Users.Where(x => x.Username == _userSession.UserName).SingleOrDefault();
                //task.CreatedBy = context.User.Find(1); // HACK: All tasks created by user 1 for now

                if (task.Users != null && task.Users.Any())
                {
                    for (var i = 0; i < task.Users.Count; ++i)
                    {
                        var user = task.Users[i];
                        var persistedUser = context.Users.Find(user.UserId);
                        if (persistedUser == null)
                        {
                            throw new ChildObjectNotFoundException("User not found");
                        }

                        task.Users[i] = persistedUser;
                    }
                }
                context.Tasks.Add(task);
                context.SaveChanges();
            }
        }

        public Task AddTaskUser(long taskId, long userId)
        {
            using (TaskManagementDbContext context = new TaskManagementDbContext())
            {
                var task = GetValidTask(context, taskId);

                UpdateTaskUsers(context, task, new[] { userId }, true);

                // context.Tasks.Attach(task);
                //  context.Entry(task).State = EntityState.Modified;
                context.SaveChanges();

                return task;
            }
        }

        public Task DeleteTaskUser(long taskId, long userId)
        {
            using (TaskManagementDbContext context = new TaskManagementDbContext())
            {
                var task = GetValidTask(context, taskId);

                var user = task.Users.FirstOrDefault(x => x.UserId == userId);

                if (user != null)
                {
                    task.Users.Remove(user);
                    // context.Tasks.Attach(task);
                    //  context.Entry(task).State = EntityState.Modified;
                    context.SaveChanges();
                }
                return task;
            }
        }

        public Task DeleteTaskUsers(long taskId)
        {
            using (TaskManagementDbContext context = new TaskManagementDbContext())
            {
                var task = GetValidTask(context, taskId);

                UpdateTaskUsers(context, task, null, false);

                context.SaveChanges();

                return task;
            }
        }

        public Task GetTask(long taskId)
        {
            using (TaskManagementDbContext context = new TaskManagementDbContext())
            {
                var task = context.Tasks.Find(taskId);
                return task;
            }
        }

        public Task GetFullyLoadedTask(long taskId)
        {
            using (TaskManagementDbContext context = new TaskManagementDbContext())
            {
                var task = context.Tasks
                    .Include(t => t.Status)
                    .Include(t => t.CreatedBy)
                    .Include(t => t.Users)
                    .FirstOrDefault(t => t.TaskId == taskId);

                return task;
            }
        }

        public QueryResult<Task> GetTasks(PagedDataRequest requestInfo)
        {
            using (TaskManagementDbContext context = new TaskManagementDbContext())
            {
                var query = context.Tasks;  //  _session.QueryOver<Task>();

                var totalItemCount = query.Count();

                var startIndex = ResultsPagingUtility.CalculateStartIndex(requestInfo.PageNumber, requestInfo.PageSize);

                var tasks = query.Skip(startIndex).Take(requestInfo.PageSize).ToList();

                var queryResult = new QueryResult<Task>(tasks, totalItemCount, requestInfo.PageSize);

                return queryResult;
            }
        }

        public Task GetUpdatedTask(long taskId, Dictionary<string, object> updatedPropertyValueMap)
        {
            using (TaskManagementDbContext context = new TaskManagementDbContext())
            {
                var task = GetValidTask(context, taskId);

                var propertyInfos = typeof(Task).GetProperties();
                foreach (var propertyValuePair in updatedPropertyValueMap)
                {
                    propertyInfos.Single(x => x.Name == propertyValuePair.Key)
                        .SetValue(task, propertyValuePair.Value);
                }

                context.SaveChanges();

                return task;
            }
        }

        public User GetUser(string name)
        {
            using (TaskManagementDbContext context = new TaskManagementDbContext())
            {
                var user = context.Users.Where(x => x.Username == name).SingleOrDefault();
                return user;
            }
        }

        public Task ReplaceTaskUsers(long taskId, IEnumerable<long> userIds)
        {
            using (TaskManagementDbContext context = new TaskManagementDbContext())
            {
                var task = GetValidTask(context, taskId);

                UpdateTaskUsers(context, task, userIds, false);

                context.SaveChanges();

                return task;
            }
        }

        public void UpdateTaskStatus(Task taskToUpdate, string statusName)
        {
            using (TaskManagementDbContext context = new TaskManagementDbContext())
            {
                var status = context.Status.Where(x => x.Name == statusName).SingleOrDefault();

                taskToUpdate.Status = status;
                taskToUpdate.StatusId = status.StatusId;
               // context.Tasks.Attach(taskToUpdate);
               // context.Entry(taskToUpdate).State = EntityState.Modified;
              
                var entity = context.Tasks.Find(taskToUpdate.TaskId);
                context.Entry(entity).CurrentValues.SetValues(taskToUpdate);

                context.SaveChanges();
            }
        }

        private Task GetValidTask(TaskManagementDbContext context, long taskId)
        {
            var task = context.Tasks
                   .Include(t => t.Status)
                   .Include(t => t.CreatedBy)
                   .Include(t => t.Users)
                   .FirstOrDefault(t => t.TaskId == taskId);

            if (task == null)
            {
                throw new RootObjectNotFoundException("Task not found");
            }

            return task;
        }

        private User GetValidUser(TaskManagementDbContext context, long userId)
        {
            var user = context.Users.Find(userId);
            if (user == null)
            {
                throw new ChildObjectNotFoundException("User not found");
            }

            return user;
        }

        private void UpdateTaskUsers(TaskManagementDbContext context, Task task, IEnumerable<long> userIds, bool appendToExisting)
        {
            if (!appendToExisting)
            {
                task.Users.Clear();
            }

            if (userIds != null)
            {
                foreach (long userId in userIds)
                {
                    User user = GetValidUser(context, userId);

                    if (!task.Users.Contains(user))
                    {
                        task.Users.Add(user);
                    }
                }
            }
        }


    }
}
