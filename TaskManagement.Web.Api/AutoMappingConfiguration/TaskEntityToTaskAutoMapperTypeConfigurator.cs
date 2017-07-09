using AutoMapper;
using TaskManagement.Common;
using TaskManagement.Data.SqlServer.DataEntities;

namespace TaskManagement.Web.Api
{
    public class TaskEntityToTaskAutoMapperTypeConfigurator : IAutoMapperTypeConfigurator
    {
        public void Configure()
        {
            Mapper.CreateMap<Task, TaskManagement.BusinessService.Task>()
                .ForMember(opt => opt.Links, x => x.Ignore())
                .ForMember(opt => opt.Assignees, x => x.ResolveUsing<TaskAssigneesResolver>());
        }
    }
}