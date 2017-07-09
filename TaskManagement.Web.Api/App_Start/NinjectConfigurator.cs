using log4net.Config;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Ninject.Activation;
using Ninject.Web.Common;

using TaskManagement.Common;
using TaskManagement.BusinessService;
using TaskManagement.Data.SqlServer;   // because of ninject configuration
using TaskManagement.BusinessServices.Processors;


namespace TaskManagement.Web.Api
{
    public class NinjectConfigurator
    {
        public void Configure(IKernel container)
        {
            AddBindings(container);
        }
        private void AddBindings(IKernel container)
        {
            ConfigureLog4net(container);
            ConfigureUserSession(container);
            ConfigureAutoMapper(container);

            container.Bind<IDateTime>().To<DateTimeAdapter>().InSingletonScope();
          
            container.Bind<IPagedDataRequestFactory>().To<PagedDataRequestFactory>().InSingletonScope();
            container.Bind<IUpdateablePropertyDetector>().To<JObjectUpdateablePropertyDetector>().InSingletonScope();
            container.Bind<IBasicSecurityService>().To<BasicSecurityService>().InSingletonScope();

            container.Bind<ITaskMaintenanceProcessor>().To<TaskMaintenanceProcessor>().InRequestScope();
            container.Bind<IAllTasksInquiryProcessor>().To<AllTasksInquiryProcessor>().InRequestScope();
     
            container.Bind<ITaskDAS>().To<TaskRepository>().InRequestScope();

            container.Bind<ITaskLinkService>().To<TaskLinkService>().InRequestScope();
            container.Bind<IUserLinkService>().To<UserLinkService>().InRequestScope();
            container.Bind<ICommonLinkService>().To<CommonLinkService>().InRequestScope();
        }

        private void ConfigureAutoMapper(IKernel container)
        {
            container.Bind<IAutoMapper>().To<AutoMapperAdapter>().InSingletonScope();

            container.Bind<IAutoMapperTypeConfigurator>()
            .To<StatusEntityToStatusAutoMapperTypeConfigurator>()
            .InSingletonScope();
            container.Bind<IAutoMapperTypeConfigurator>()
            .To<StatusToStatusEntityAutoMapperTypeConfigurator>()
            .InSingletonScope();
            container.Bind<IAutoMapperTypeConfigurator>()
            .To<UserEntityToUserAutoMapperTypeConfigurator>()
            .InSingletonScope();
            container.Bind<IAutoMapperTypeConfigurator>()
            .To<UserToUserEntityAutoMapperTypeConfigurator>()
            .InSingletonScope();
            container.Bind<IAutoMapperTypeConfigurator>()
            .To<NewTaskToTaskEntityAutoMapperTypeConfigurator>()
            .InSingletonScope();
            container.Bind<IAutoMapperTypeConfigurator>()
            .To<TaskEntityToTaskAutoMapperTypeConfigurator>()
            .InSingletonScope();
        }

        private void ConfigureUserSession(IKernel container)
        {
            var userSession = new UserSession();
            container.Bind<IUserSession>().ToConstant(userSession).InSingletonScope();
            container.Bind<IWebUserSession>().ToConstant(userSession).InSingletonScope();
        }

        private void ConfigureLog4net(IKernel container)
        {
            XmlConfigurator.Configure();
            var logManager = new LogManagerAdapter();
            container.Bind<ILogManager>().ToConstant(logManager);
        }

    }
}