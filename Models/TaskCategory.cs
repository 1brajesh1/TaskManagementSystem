using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManagementSystem.Models
{
    public class TaskCategory
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please Enter the name of Tasks Category ")]
        public string Name { get; set; }

        //[Required]
        //public List<ViewTask> Tasks { get; set; }

        //public List<string> Employees { get; set; }

        public ApplicationUser Manager { get; set; }

        [Required(ErrorMessage ="Please specify the name of the Manager")]
        [Display(Name = "Manager")]
        [ForeignKey("Manager")]
        public string ManagerId { get; set; }

        //public string JointUsers { get; set; }

        public List<Task> Tasks { get; set; }
    }
}
