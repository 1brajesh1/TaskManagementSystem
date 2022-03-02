using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TaskManagementSystem.Enums;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.ViewModels
{
    public class ListViewModel
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "Please specify the Name of the Tasks")]
        [Display(Name = "Tasks Name")]
        public string TaskName { get; set; }



        [Required(ErrorMessage = "Please specify the Task description")]
        [Display(Name = "Tasks Details")]
        public string TaskDetails { get; set; }



        [Required(ErrorMessage = "Please fill out this field")]
        //[DataType(DataType.Date)]
        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; } = DateTime.Now; //today date always 



        [Required(ErrorMessage = "Please specify the Assigned date of the task")]
        //[DataType(DataType.Date)]
        [Display(Name = "Assigned Date")]
        public DateTime AssignedDate { get; set; } = DateTime.Now;



        [Required(ErrorMessage = "Please specify the Due date of the task")]
        //[DataType(DataType.Date)]
        [Display(Name = "Due Date")]
        public DateTime DueDate { get; set; } = DateTime.Now;



        //public string TaskType { get; set; }


        //public string TaskStatus { get; set; }


        [Required(ErrorMessage = "Please specify who assigned the task")]
        [Display(Name = "Tasks Status")]
        public TaskStatusEnum TaskStatus { get; set; }




        [Required(ErrorMessage = "Please specify the Completed Date")]
        [Display(Name = "Completed Date")]
        public DateTime TaskCompletedDate { get; set; } = DateTime.Now;

        //[ForeignKey("TaskCategory")]

        //[Display(Name = "Tasks Category")]
        //[ForeignKey("TaskCategory")]
        //public int TaskCategoryId { get; set; }




        //[Required(ErrorMessage = "Please fill out this field")]
        //[Display(Name = "Assigned To")]
        //public List<UsersTask> UserTasks { get; set; }


        //[Required(ErrorMessage = "Please specify who assigned the task")]
        //[Display(Name = "Assigned By")]
        //public IdentityUser AssignedBy { get; set; } //manager

        
        [Display(Name = "Assigned By")]
        public string AssignedById { get; set; }

        public ApplicationUser Assignedby { get; set; }



        //public List<ApplicationUser> AssignedTo { get; set; }


        
        [Display(Name = "Tasks Categories")]
        public int? TaskCategoryId { get; set; }
        public TaskCategory TaskCategory { get; set; }

        //public List<SelectListItem> AssignedTo { get; set; }

        public List<ApplicationUser> AssignedTo { get; set; } = new List<ApplicationUser>();
        [Display(Name = "Assigned To")]
        public string[] AssignedToIds { get; set; }


    }
}
