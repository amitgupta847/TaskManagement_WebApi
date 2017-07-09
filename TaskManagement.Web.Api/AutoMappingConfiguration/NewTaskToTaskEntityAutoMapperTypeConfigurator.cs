using AutoMapper;
using TaskManagement.BusinessService;
using TaskManagement.Common;
using Task = TaskManagement.Data.SqlServer.DataEntities.Task;

namespace TaskManagement.Web.Api
{
    public class NewTaskToTaskEntityAutoMapperTypeConfigurator : IAutoMapperTypeConfigurator
    {
        public void Configure()
        {
            Mapper.CreateMap<NewTask, Task>()
                .ForMember(opt => opt.Version, x => x.Ignore())
                .ForMember(opt => opt.CreatedBy, x => x.Ignore())
                .ForMember(opt => opt.TaskId, x => x.Ignore())
                .ForMember(opt => opt.CreatedDate, x => x.Ignore())
                .ForMember(opt => opt.CompletedDate, x => x.Ignore())
                .ForMember(opt => opt.Status, x => x.Ignore())
                .ForMember(opt => opt.Users, x => x.Ignore())
                .ForMember(opt => opt.StatusId, x => x.Ignore());
        }
    }
}