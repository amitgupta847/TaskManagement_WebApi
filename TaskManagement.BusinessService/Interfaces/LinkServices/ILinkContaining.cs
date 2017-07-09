
using System.Collections.Generic;


namespace TaskManagement.BusinessService
{
    public interface ILinkContaining
    {
        List<Link> Links { get; set; }
        void AddLink(Link link);
    }
}