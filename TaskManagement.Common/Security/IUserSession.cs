using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace TaskManagement.Common
{
   public interface IUserSession
    {
        string FirstName { get; }
        string LastName { get; }
        string UserName { get; }
        bool IsInRole(string roleName);
    }


}
