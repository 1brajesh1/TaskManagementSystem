using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        public IActionResult Create()
        {


            //var usersList = _context.Users.ToList();

            var usersList = _context.Users.AsNoTracking().ToList();

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
            var assignedToList = _context.Users.AsNoTracking().ToList();
            var assignedToLists = new List<SelectListItem>();

            foreach (var i in assignedToList)
            {
                SelectListItem userListss = new SelectListItem { Value = i.Id, Text = i.UserName };
                assignedToLists.Add(userListss);
            }

            TaskViewModel tvm = new TaskViewModel()
            {
                AssignedBy = users,
                AssignedTo = assignedToLists,
                TaskCategories = catergories

            };

            return View(tvm);
        }

        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TaskViewModel taskViewModel)
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

                var response = _context.Tasks.Add(task);
                _context.SaveChanges();

                if (response.Entity.Id != 0)
                {

                    foreach (var item in taskViewModel.AssignedToIds)
                    {
                        UsersTask tsk = new UsersTask();
                        tsk.TaskId = response.Entity.Id;
                        tsk.ApplicationUserId = item;
                        _context.UsersTasks.Add(tsk);
                        _context.SaveChanges();
                    }

                }

                return RedirectToAction("ViewTask");
            }
            return View(taskViewModel);

        }
        
        public ActionResult ViewTask()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var userId = claim.Value;
            if (User.IsInRole("Admin"))

            {
                var TaskLists = _context.Tasks.Include(a => a.UserTasks).ToList();

                var allTaskList = TaskLists.Select(c => new ListViewModel()
                {
                    Id = c.Id,
                    TaskName = c.TaskName,

                    TaskDetails = c.TaskDetails,
                    TaskStatus = c.TaskStatus,
                    TaskCategoryId = c.TaskCategoryId,
                    AssignedDate = c.AssignedDate,
                    AssignedById = c.AssignedById,
                    DueDate = c.DueDate,
                    TaskCompletedDate = c.TaskCompletedDate,
                    CreatedDate = c.CreatedDate,
                    AssignedToIds = c.UserTasks.Select(c => c.ApplicationUserId).ToArray(),

                }).ToList();



                foreach (var item in allTaskList)
                {
                    var assignedby = _context.Users.Find(item.AssignedById);

                    item.Assignedby = assignedby;

                    var taskcategory = _context.TaskCategories.Find(item.TaskCategoryId);
                    item.TaskCategory = taskcategory;

                    //var list = new List<SelectListItem>();

                    foreach (var items in item.AssignedToIds)
                    {

                        var users = _context.Users.Find(items);
                        item.AssignedTo.Add(users);

                    }

                }
                //var tasks = _context.Tasks.ToList();
                //return View(tasks);
                return View(allTaskList);

            }
            else if (User.IsInRole("Manager"))
            {

                var TaskLists = from cust in _context.Tasks
                                where cust.AssignedById == userId
                                select cust;

                var allTaskList = TaskLists.Select(c => new ListViewModel()
                {
                    Id = c.Id,
                    TaskName = c.TaskName,

                    TaskDetails = c.TaskDetails,
                    TaskStatus = c.TaskStatus,
                    TaskCategoryId = c.TaskCategoryId,
                    AssignedDate = c.AssignedDate,
                    AssignedById = c.AssignedById,
                    DueDate = c.DueDate,
                    TaskCompletedDate = c.TaskCompletedDate,
                    CreatedDate = c.CreatedDate,
                    AssignedToIds = c.UserTasks.Select(c => c.ApplicationUserId).ToArray(),
                }).ToList();


                foreach (var item in allTaskList)
                {
                    var assignedby = _context.Users.Find(item.AssignedById);

                    item.Assignedby = assignedby;

                    var taskcategory = _context.TaskCategories.Find(item.TaskCategoryId);
                    item.TaskCategory = taskcategory;


                    foreach (var items in item.AssignedToIds)
                    {

                        var users = _context.Users.Find(items);
                        item.AssignedTo.Add(users);


                    }


                }
                return View(allTaskList);

            }
            else
            {
                //var TaskLists = _context.Tasks.Include(a => a.UserTasks).ToList();
                //var allTaskList = TaskLists.Select(c => new ListViewModel()

                var TaskLists = _context.UsersTasks.Include(a => a.ApplicationUser).Include(a => a.Task)
                    .Where(a => a.ApplicationUserId == userId).Select(c => new ListViewModel()

                    {
                        Id = c.Task.Id,
                        TaskName = c.Task.TaskName,

                        TaskDetails = c.Task.TaskDetails,
                        TaskStatus = c.Task.TaskStatus,
                        TaskCategoryId = c.Task.TaskCategoryId,
                        AssignedDate = c.Task.AssignedDate,
                        AssignedById = c.Task.AssignedById,
                        DueDate = c.Task.DueDate,
                        TaskCompletedDate = c.Task.TaskCompletedDate,
                        CreatedDate = c.Task.CreatedDate,
                        AssignedToIds = c.Task.UserTasks.Select(c => c.ApplicationUserId).ToArray(),

                    }).ToList();
                foreach (var item in TaskLists)
                {
                    var assignedby = _context.Users.Find(item.AssignedById);

                    item.Assignedby = assignedby;

                    var taskcategory = _context.TaskCategories.Find(item.TaskCategoryId);
                    item.TaskCategory = taskcategory;


                    foreach (var items in item.AssignedToIds)
                    {
                        if (items == userId)
                        {
                            var users = _context.Users.Find(items);
                            item.AssignedTo.Add(users);

                        }

                    }

                }

                return View(TaskLists);
            }

        }





            // GET: TaskController/Edit/5
            public ActionResult Edit(int id)
            {
                var taskViewModel = new TaskViewModel();
                var assignedTolist = _context.Tasks.Include(m => m.UserTasks).Where(x => x.Id == id).FirstOrDefault();


                var userList = _context.Users.ToList();
                var users = new List<SelectListItem>();

                foreach (var item in userList)
                {
                    SelectListItem userLists = new SelectListItem { Value = item.Id, Text = item.UserName };
                    users.Add(userLists);
                }

                var categoriesList = _context.TaskCategories.ToList();
                var categories = new List<SelectListItem>();
                foreach (var items in categoriesList)
                {
                    SelectListItem categorylist = new SelectListItem { Value = items.Id.ToString(), Text = items.Name };
                    categories.Add(categorylist);
                }
                var assignedToList = _context.Users.ToList();
                var assignedToLists = new List<SelectListItem>();

                foreach (var i in assignedToList)
                {
                    SelectListItem userListss = new SelectListItem { Value = i.Id, Text = i.UserName };
                    assignedToLists.Add(userListss);
                }

                taskViewModel.TaskCategories = categories;
                taskViewModel.AssignedBy = users;
                taskViewModel.AssignedTo = assignedToLists;
                taskViewModel.TaskName = assignedTolist.TaskName;
                taskViewModel.TaskDetails = assignedTolist.TaskDetails;
                taskViewModel.TaskCategoriesId = assignedTolist.TaskCategoryId;
                taskViewModel.TaskStatus = assignedTolist.TaskStatus;
                taskViewModel.AssignedById = assignedTolist.AssignedById;
                taskViewModel.AssignedDate = assignedTolist.AssignedDate;
                taskViewModel.DueDate = assignedTolist.DueDate;
                taskViewModel.TaskCompletedDate = assignedTolist.TaskCompletedDate;
                taskViewModel.CreatedDate = assignedTolist.CreatedDate;
                taskViewModel.AssignedToIds = assignedTolist.UserTasks.Select(x => x.ApplicationUserId).ToArray();

                return View(taskViewModel);



            }



            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Edit(TaskViewModel taskViewModel)
            {
                if (ModelState.IsValid)
                {
                    var editTask = _context.Tasks.AsNoTracking().Include(m => m.UserTasks).Where(x => x.Id == taskViewModel.Id).FirstOrDefault();

                    editTask.TaskName = taskViewModel.TaskName;
                    editTask.TaskDetails = taskViewModel.TaskDetails;
                    editTask.TaskCategoryId = taskViewModel.TaskCategoriesId;
                    editTask.TaskStatus = taskViewModel.TaskStatus;

                    editTask.AssignedById = taskViewModel.AssignedById;
                    editTask.AssignedDate = taskViewModel.AssignedDate;
                    editTask.DueDate = taskViewModel.DueDate;
                    editTask.TaskCompletedDate = taskViewModel.TaskCompletedDate;
                    editTask.CreatedDate = taskViewModel.CreatedDate;


                    var response = _context.Tasks.Update(editTask);

                    _context.SaveChanges();
                    _context.Entry(editTask).State = EntityState.Detached;


                    if (response.Entity.Id != 0)
                    {

                        var editAssignedTo = _context.UsersTasks.AsNoTracking().Where(x => x.TaskId == response.Entity.Id).ToArray();


                        _context.UsersTasks.RemoveRange(editAssignedTo);
                        _context.SaveChanges();




                        foreach (var item in taskViewModel.AssignedToIds)
                        {
                            UsersTask tsk = new UsersTask();
                            tsk.TaskId = response.Entity.Id;
                            tsk.ApplicationUserId = item;
                            _context.UsersTasks.Add(tsk);
                            _context.SaveChanges();
                        }





                        _context.SaveChanges();
                    }

                    return RedirectToAction("ViewTask");
                }


                return RedirectToAction("ViewTask");
            }





            public ActionResult Details(int id)
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
                //return View(taskdetails);
                return PartialView("_DetailTask", taskdetails);

            }



        // POST: TaskController/Delete/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                //var userTasks = _context.UsersTasks
                var task = _context.Tasks.Include(u=>u.UserTasks).Where(a => a.Id == id).FirstOrDefault();
                task.UserTasks.Clear();
                _context.Tasks.Remove(task);
                _context.SaveChanges();
                //TempData["Success"] = "Cateogry deleted successfully";
                return Json(true);
            }
            catch(Exception ex)
            {
                //ViewBag.ErrorMessage = "This category is linked with task hence cannot be deleted";
                return Json(false);
            }
        }
    }
}
