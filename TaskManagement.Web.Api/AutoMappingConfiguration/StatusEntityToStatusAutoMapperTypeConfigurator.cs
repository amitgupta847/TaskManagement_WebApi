using AutoMapper;
using TaskManagement.Common;
using TaskManagement.Data.SqlServer.DataEntities;

namespace TaskManagement.Web.Api
{
    public class StatusEntityToStatusAutoMapperTypeConfigurator : IAutoMapperTypeConfigurator
    {
        public void Configure()
        {
            Mapper.CreateMap<Status,  TaskManagement.BusinessService.Status>();
        }
    }
}