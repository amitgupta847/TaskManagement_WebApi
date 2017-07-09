using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Common
{
    public interface IAutoMapper
    {
        T Map<T>(object objectToMap);
    }
}
