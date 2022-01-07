using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TaskManagementSystem.Enums;

namespace TaskManagementSystem.Models
{
    public class Task
    {
        [Key]
        public int TaskId { get; set; }
        [Required]
        public string TaskName { get; set; }
        [Required]
        public string TaskDetails { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        public DateTime AssignedDate { get; set; }
        public DateTime DueDate { get; set; }
        //public string TaskType { get; set; }

        public string AssignedBy { get; set; }
        //public List<string> AssignedTo { get; set; }
        //public string TaskStatus { get; set; }

        public TaskStatusEnum TaskStatusEnum { get; set; }

        public DateTime TaskCompletedDate { get; set; }

    




    }
}
