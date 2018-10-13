using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Entity
{
    public class ProjectEntity
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public Nullable<decimal> ProjectPriority { get; set; }
        public string DateReqd { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<int> ManagerId { get; set; }
        public string ProjectStatus { get; set; }
        public Nullable<System.DateTime> AddDate { get; set; }
        public Nullable<System.DateTime> UpdtDate { get; set; }
        public string ManagerName { get; set; }
    }
}
