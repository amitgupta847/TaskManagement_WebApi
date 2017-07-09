using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.BusinessService
{
    public class Link
    {
        public string Rel { get; set; }
        public string Href { get; set; }
        public string Method { get; set; }
    }

    public class Status
    {
        public long StatusId { get; set; }
        public string Name { get; set; }
        public int Ordinal { get; set; }
    }

    public class Task : ILinkContaining
    {
        private List<Link> _links;
        private bool _shouldSerializeAssignees;

        [Key]
        public long? TaskId { get; set; }

        [Editable(true)]
        public string Subject { get; set; }

        [Editable(true)]
        public DateTime? StartDate { get; set; }

        [Editable(true)]
        public DateTime? DueDate { get; set; }

        [Editable(false)]
        public DateTime? CreatedDate { get; set; }

        [Editable(false)]
        public DateTime? CompletedDate { get; set; }

        [Editable(false)]
        public Status Status { get; set; }

        public long StatusId { get; set; }

        [Editable(false)]
        public List<User> Assignees { get; set; }

        [Editable(false)]
        public List<Link> Links
        {
            get { return _links ?? (_links = new List<Link>()); }
            set { _links = value; }
        }

        public void AddLink(Link link)
        {
            Links.Add(link);
        }

        public void SetShouldSerializeAssignees(bool shouldSerialize)
        {
            _shouldSerializeAssignees = shouldSerialize;
        }

        public bool ShouldSerializeAssignees()
        {
            return _shouldSerializeAssignees;
        }
    }

    public class NewTask
    {
        [Required(AllowEmptyStrings = false)]
        public string Subject { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? DueDate { get; set; }
        public List<User> Assignees { get; set; }
    }

    public class NewTaskV2
    {
        public string Subject { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? DueDate { get; set; }
        public User Assignee { get; set; }
    }

    public class User
    {
        private List<Task> _tasks;

        private List<Link> _links;
        public long UserId { get; set; }
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public List<Task> Tasks
        {
            get { return _tasks ?? (_tasks = new List<Task>()); }
            set { _tasks = value; }
        }

        public List<Link> Links
        {
            get { return _links ?? (_links = new List<Link>()); }
            set { _links = value; }
        }
        public void AddLink(Link link)
        {
            Links.Add(link);
        }
    }


}

