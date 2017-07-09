using System;
using System.Collections.Generic;

namespace TaskManagement.Data.SqlServer.DataEntities
{
    public class Status : IVersionedEntity
    {
        public long StatusId { get; set; }
        public string Name { get; set; }
        public int Ordinal { get; set; }
        public virtual byte[] Version { get; set; }
    }

    public class Task : IVersionedEntity
    {
        public Task()
        {
            Users = new List<User>();
        }
        public long TaskId { get; set; }
        public string Subject { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? CompletedDate { get; set; }

        public Status Status { get; set; }

        public long StatusId { get; set; }

        public DateTime? CreatedDate { get; set; }
       
        public User CreatedBy { get; set; }

        public IList<User> Users { get; set; }

         public virtual byte[] Version { get; set; }
    }

    public class User : IVersionedEntity
    {
        public long UserId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public byte[] Version { get; set; }

        public virtual IList<Task> Tasks { get; set; }

        public User()
        {
            Tasks = new List<Task>();
        }
    }


   


}

