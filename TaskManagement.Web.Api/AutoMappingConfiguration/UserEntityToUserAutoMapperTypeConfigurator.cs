using AutoMapper;
using TaskManagement.Common;
using TaskManagement.Data.SqlServer.DataEntities;

namespace TaskManagement.Web.Api
{
    public class UserEntityToUserAutoMapperTypeConfigurator : IAutoMapperTypeConfigurator
    {
        public void Configure()
        {
            Mapper.CreateMap<User, TaskManagement.BusinessService.User>()
                .ForMember(opt => opt.Links, x => x.Ignore())
                .ForMember(opt => opt.Tasks, x => x.Ignore());
        }
    }
}