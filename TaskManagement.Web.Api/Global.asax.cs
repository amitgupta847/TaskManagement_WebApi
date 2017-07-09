using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using TaskManagement.Common;

namespace TaskManagement.Web.Api
{
    public class TaskManagementApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(TaskManangementConfig.Register);

            RegisterHandlers();

            new AutoMapperConfigurator().Configure(WebContainerManager.GetAll<IAutoMapperTypeConfigurator>());
        }
        private void RegisterHandlers()
        {
            var logManager = WebContainerManager.Get<ILogManager>();
            var userSession = WebContainerManager.Get<IUserSession>();

            GlobalConfiguration.Configuration.MessageHandlers.Add(new BasicAuthenticationMessageHandler(logManager, WebContainerManager.Get<IBasicSecurityService>()));
            GlobalConfiguration.Configuration.MessageHandlers.Add(new TaskDataSecurityMessageHandler(logManager, userSession));
            GlobalConfiguration.Configuration.MessageHandlers.Add(new PagedTaskDataSecurityMessageHandler(logManager, userSession));
        }
        protected void Application_Error()
        {
            var exception = Server.GetLastError();

            if (exception != null)
            {
                var log = WebContainerManager.Get<ILogManager>().GetLog(typeof(TaskManagementApplication));
                log.Error("Unhandled exception.", exception);
            }
        }




    }
}
