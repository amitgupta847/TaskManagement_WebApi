using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TaskManagement.Common;

namespace TaskManagement.Web.Api
{
    public class UserSession : IWebUserSession
    {
        public string ApiVersionInUse
        {
            get
            {
                const int versionIndex = 2;
                if (HttpContext.Current.Request.Url.Segments.Count() < versionIndex + 1)
                {
                    return string.Empty;
                }

                var apiVersionInUse = HttpContext.Current.Request.Url.Segments[versionIndex].Replace( "/", string.Empty);
                return apiVersionInUse;
            }
        }

        public string FirstName
        {
            get { return ((ClaimsPrincipal)HttpContext.Current.User).FindFirst(ClaimTypes.GivenName).Value; }
        }

        public string HttpRequestMethod
        {
            get { return HttpContext.Current.Request.HttpMethod; }
        }

        public string LastName
        {
            get { return ((ClaimsPrincipal)HttpContext.Current.User).FindFirst(ClaimTypes.Surname).Value; }
        }

        public Uri RequestUri
        {
            get { return HttpContext.Current.Request.Url; }
        }

        public string UserName
        {
            get { return ((ClaimsPrincipal)HttpContext.Current.User).FindFirst(ClaimTypes.Name).Value; }
        }

        public bool IsInRole(string roleName)
        {
            return HttpContext.Current.User.IsInRole(roleName);
        }
    }
}
