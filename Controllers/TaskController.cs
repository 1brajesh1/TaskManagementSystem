using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagementSystem.Data;
using TaskManagementSystem.Models;
using Task = TaskManagementSystem.Models.Task;

namespace TaskManagementSystem.Controllers
{
    public class TaskController : Controller
    {
        private readonly ILogger<TaskController> _logger;
        public ApplicationDbContext _context;

        public TaskController(ILogger<TaskController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
            

         public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> CreateTask()
        {
            // var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) // will give the user's userId
            //var userName = User.FindFirstValue(ClaimTypes.Name) // will give the user's userName

            var usersList = _context.Users.ToList();

            List<SelectListItem> users = new List<SelectListItem>();
           
            foreach (var item in usersList)
            {
                SelectListItem userSelectList = new SelectListItem { Value = item.Id, Text = item.UserName };
                users.Add(userSelectList);
            }

            //assigning SelectListItem to view Bag
            ViewBag.users = users;

            return View();
        }
        [HttpPost]
        public IActionResult CreateTask(Task task)
        {
            _context.Tasks.Add(task);
            _context.SaveChanges();
            //return View();
            return RedirectToAction("TaskAction");
        }
        public IActionResult TaskAction()
        {
            var tasks = _context.Tasks.ToList();
            return View(tasks);

        }



    }
}
