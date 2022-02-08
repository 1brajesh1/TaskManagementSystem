    using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TaskManagementSystem.Enums;

namespace TaskManagementSystem.Models
{
    public class Task
    {

        [Key]
        public int Id { get; set; }



        [Required]
        public string TaskName { get; set; }


        [Required]
        public string TaskDetails { get; set; }

        [Required]
        //[DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; } //today date always 

        [Required]
        public DateTime AssignedDate { get; set; }

        [Required]
        public DateTime DueDate { get; set; }


        //public string TaskType { get; set; }

        //[Required(ErrorMessage = "Please specify who assigned the task")]
        //[Display(Name = "Assigned By")]
        //public IdentityUser AssignedBy { get; set; } //manager

        public ApplicationUser AssignedBy { get; set; } //manager

        [Required]
        public string AssignedById { get; set; }


        //public List<string> AssignedTo { get; set; }
        //public string TaskStatus { get; set; }

        [Required]
        public TaskStatusEnum TaskStatus { get; set; }

        [Required]
        public DateTime TaskCompletedDate { get; set; }

        [ForeignKey("TaskCategory")]
        public int? TaskCategoryId { get; set; }

        public virtual TaskCategory TaskCategory { get; set; }
        //public TaskCategory TaskCategory { get; set; }

        //[Required]
        public List<ApplicationUser> AssignedTo { get; set; }

        //public List<UsersTask> UserVsTasks { get; set; }

        //public ICollection<UsersTask> UserTasks { get; set; } = new HashSet<UsersTask>();

        public ICollection<UsersTask> UserTasks { get; set; }



    }
}
