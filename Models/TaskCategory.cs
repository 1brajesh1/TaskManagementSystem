using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManagementSystem.Models
{
    public class TaskCategory
    {
        public int TaskCategoryId { get; set; }
        public string Name { get; set; }

        public List<Task> Tasks { get; set; }

        //public List<string> Employees { get; set; }

        public string Manager { get; set; }

        //public string JointUsers { get; set; }
    }
}
