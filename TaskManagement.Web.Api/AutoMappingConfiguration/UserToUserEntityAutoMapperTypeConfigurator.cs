
using AutoMapper;
using TaskManagement.BusinessService;
using TaskManagement.Common;

namespace TaskManagement.Web.Api
{
    public class UserToUserEntityAutoMapperTypeConfigurator : IAutoMapperTypeConfigurator
    {
        public void Configure()
        {
            Mapper.CreateMap<User, TaskManagement.Data.SqlServer.DataEntities.User>()
                .ForMember(opt => opt.Version, x => x.Ignore())
                .ForMember(opt => opt.Tasks, x => x.Ignore());
        }
    }
}