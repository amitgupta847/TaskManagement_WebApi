using System.Collections.Generic;
using System.Linq;
using AutoMapper;

using User = TaskManagement.BusinessService.User;
using TaskManagement.Data.SqlServer.DataEntities;
using TaskManagement.Common;

namespace TaskManagement.Web.Api
{
    public class TaskAssigneesResolver : ValueResolver<Task, List<User>>
    {
        public IAutoMapper AutoMapper
        {
            get { return WebContainerManager.Get<IAutoMapper>(); }
        }

        protected override List<User> ResolveCore(Task source)
        {
            return source.Users.Select(x => AutoMapper.Map<User>(x)).ToList();
        }
    }
}