using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManagementSystem.Models
{
    public class UserVsTask
    {
        //public ViewTask ViewTask { get; set; }
        //public ViewTaskCategory ViewTaskCategory { get; set; }

        public int Id { get; set; }

        public string ApplicationUserId { get; set; }
       

        public ApplicationUser ApplicationUser { get; set; }

        public int TaskId { get; set; }

        public Task Task { get; set; }
    }
}
