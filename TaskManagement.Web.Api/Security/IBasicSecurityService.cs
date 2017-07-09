using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManagement.Web.Api
{
    public interface IBasicSecurityService
    {
        bool SetPrincipal(string username, string password);
    }
}