using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Entity
{
    public class TaskEntity
    {
        public int TaskId { get; set; }
        public string Task1 { get; set; }
        public Nullable<decimal> TaskPriority { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public string TaskStatus { get; set; }
        public Nullable<int> ParentId { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<System.DateTime> AddDate { get; set; }
        public Nullable<System.DateTime> UpdtDate { get; set; }
    }
}
