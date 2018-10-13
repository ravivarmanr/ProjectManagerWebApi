using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Entity
{
    public class ParentTaskEntity
    {
        public int ParentId { get; set; }
        public string ParentTask { get; set; }
        public string ParentStatus { get; set; }
        public Nullable<System.DateTime> AddDate { get; set; }
        public Nullable<System.DateTime> UpdtDate { get; set; }
    }
}
