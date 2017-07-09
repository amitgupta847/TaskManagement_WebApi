using AutoMapper;
using TaskManagement.BusinessService;
using TaskManagement.Common;

namespace TaskManagement.Web.Api
{
    public class StatusToStatusEntityAutoMapperTypeConfigurator : IAutoMapperTypeConfigurator
    {
        public void Configure()
        {
            Mapper.CreateMap<Status, TaskManagement.Data.SqlServer.DataEntities.Status>()
                .ForMember(opt => opt.Version, x => x.Ignore());
        }
    }
}