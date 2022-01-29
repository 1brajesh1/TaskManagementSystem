using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagementSystem.Data;
using TaskManagementSystem.Models;
using TaskManagementSystem.ViewModels;


namespace TaskManagementSystem.Controllers
{
    [Authorize]
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

        [Authorize]
        [Authorize(Roles = "Admin,Manager")]
        public IActionResult CreateTask()
        {
            // var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) // will give the user's userId
            //var userName = User.FindFirstValue(ClaimTypes.Name) // will give the user's userName

            var usersList = _context.Users.ToList();

           var users = new List<SelectListItem>();

            foreach (var item in usersList)
            {
                var userSelectList = new SelectListItem { Value = item.Id, Text = item.UserName };
                users.Add(userSelectList);
            }

            var categoriesList = _context.TaskCategories.ToList();
            var catergories = new List<SelectListItem>();

            foreach (var items in categoriesList)
            {
                SelectListItem categorylist = new SelectListItem { Value = items.Id.ToString(), Text = items.Name };
                catergories.Add(categorylist);

            }
            TaskViewModel tvm = new TaskViewModel()
            {
                AssignedBy = users,
                AssignedTo = users,
                TaskCategories = catergories
            };

            //assigning SelectListItem to view Bag
            //ViewBag.users = users;

            return View(tvm);
        }

        //For assigned by field. To access data from database and show it in dropdownlist

        //[HttpPost]
        //public IActionResult CreateTask(Tasks task)
        //{
        //    _context.Tasks.Add(task);
        //    _context.SaveChanges();        
        //    return RedirectToAction("ViewTask");
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateTask(TaskViewModel taskViewModel)
        {
            if (ModelState.IsValid)
            {
                Models.Task task = new Models.Task()
                {
                    TaskName = taskViewModel.TaskName,
                    TaskDetails = taskViewModel.TaskDetails,
                    TaskCategoryId = taskViewModel.TaskCategoriesId,
                    AssignedDate = taskViewModel.AssignedDate,
                    AssignedById = taskViewModel.AssignedById,
                    //AssignedTo = taskViewModel.AssignedTo,
                    TaskStatus = taskViewModel.TaskStatus,
                    CreatedDate = taskViewModel.CreatedDate,
                    DueDate = taskViewModel.DueDate,
                    TaskCompletedDate = taskViewModel.TaskCompletedDate

                };
                //var selectedUsers = taskViewModel.AssignedTo.Where(x => x.Selected).Select(y => y.Value).ToList();
                //foreach (var item in selectedUsers)
                //{
                //    task.UserTasks.Add(new UsersTask()
                //    {

                //        ApplicationUserId = item
                //    });
                //}
                _context.Add(task);
                _context.SaveChanges();
                return RedirectToAction("ViewTask");

            }
            return View(taskViewModel);
        }
        public IActionResult ViewTask()
        {

            var TaskLists = _context.Tasks.ToList();
            foreach (var item in TaskLists)
            {
                var assignedby = _context.Users.Find(item.AssignedById);

                item.AssignedBy = assignedby;

                var taskcategory = _context.TaskCategories.Find(item.TaskCategory);
                item.TaskCategory = taskcategory;
            }

            //var tasks = _context.Tasks.ToList();
            //return View(tasks);
            return View(TaskLists);

        }
       


        // GET: TaskCategoryController/Edit/5
        public ActionResult Edit(int id)
        {
            var task1 = _context.Tasks.Find(id);
             
            var usersList = _context.Users.ToList();

            var users = new List<SelectListItem>();

            foreach (var item in usersList)
            {
                var userSelectList = new SelectListItem { Value = item.Id, Text = item.UserName };
                users.Add(userSelectList);
            }

            //assigning SelectListItem to view Bag
            ViewBag.users = users;

          
            var categoriesList = _context.TaskCategories.ToList();

            var categories = new List<SelectListItem>();

            foreach (var items in categoriesList)
            {
                var categorySelectList = new SelectListItem { Value = items.Id.ToString(), Text = items.Name };
                users.Add(categorySelectList);
            }

            //assigning SelectListItem to view Bag
            ViewBag.categories = categories;


            return View(task1);
        }

        // POST: TaskCategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Models.Task task1)
        {
            _context.Tasks.Update(task1);
            await _context.SaveChangesAsync();
            //return View();
            return RedirectToAction("ViewTask");

        }
        public IActionResult Details(int id)
        {
            
            
                var taskdetails = _context.Tasks.Find(id);

                //var taskdetails = await _context.Tasks.FirstOrDefaultAsync(Tasks => Tasks.Id == id);
                var TaskLists = _context.Tasks.ToList();
                foreach (var item in TaskLists)
                {
                    var assignedby = _context.Users.Find(item.AssignedById);
                    item.AssignedBy = assignedby;

                    var taskcategory = _context.TaskCategories.Find(item.TaskCategoryId);
                    item.TaskCategory = taskcategory;
                }
                return View(taskdetails);

            }
            // return View(tasks);
            
        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskdelete = await _context.Tasks
                .FirstOrDefaultAsync(Task => Task.Id == id);
            var TaskLists = _context.Tasks.ToList();
            foreach (var item in TaskLists)
            {
                var assignedby = _context.Users.Find(item.AssignedById);
                item.AssignedBy = assignedby;
            }

            if (taskdelete == null)
            {
                return NotFound();
            }

            return View(taskdelete);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var taskdelete = await _context.Tasks.FindAsync(id);
            _context.Tasks.Remove(taskdelete);
            await _context.SaveChangesAsync();
            return RedirectToAction("ViewTask");
        }
    }

    }

