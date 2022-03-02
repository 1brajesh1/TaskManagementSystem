using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

namespace TaskManagementSystem.Controllers
{
    //[Authorize]
    public class TaskCategoryController : Controller
    {
        
        private readonly ILogger<TaskCategoryController> _logger;
        public ApplicationDbContext _context;

        public TaskCategoryController(ILogger<TaskCategoryController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

      
        // GET: TaskCategoryController
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        [Authorize(Roles = "Admin,Manager")]

        // GET: TaskCategoryController/Create
        [HttpGet]
        public ActionResult Create()
        {
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
        

        // POST: TaskCategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]

       
        public IActionResult Create(IFormCollection collection, TaskCategory taskcategory)
        {
            _context.TaskCategories.Add(taskcategory);
            _context.SaveChanges();
            //return View();
            return RedirectToAction("ViewTaskCategory");
        }

        public IActionResult ViewTaskCategory()
        {

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var userId = claim.Value;


            if (User.IsInRole("Admin"))
            {

                var taskCatogories = _context.TaskCategories.ToList();

                foreach (var item in taskCatogories)
                {
                    var managers = _context.Users.Find(item.ManagerId);
                    item.Manager = managers;
                }

                return View(taskCatogories);

            }
            else if (User.IsInRole("Manager"))
            {
                var taskcategories = from cust in _context.TaskCategories
                                     where cust.ManagerId == userId
                                     select cust;

                foreach (var item in taskcategories)
                {
                    var managers = _context.Users.Find(item.ManagerId);
                    item.Manager = managers;

                }
                return View(taskcategories);

            }
            else
            {
                var TaskLists = _context.UsersTasks.Include(a => a.ApplicationUser).Include(a => a.Task)
                    .Where(a => a.ApplicationUserId == userId)
                    .Select(c => new TaskCategory()
                    {
                        Name = c.Task.TaskCategory.Name,
                        Manager = c.Task.TaskCategory.Manager
                    }).Distinct();

                return View(TaskLists);
            }


        }


        // GET: TaskCategoryController/Edit/5
        public ActionResult Edit(int id)

        {
            var taskcategories = _context.TaskCategories.Find(id);
            var usersList = _context.Users.ToList();

            List<SelectListItem> users = new List<SelectListItem>();

            foreach (var item in usersList)
            {
                SelectListItem userSelectList = new SelectListItem { Value = item.Id, Text = item.UserName };
                users.Add(userSelectList);
            }

            //assigning SelectListItem to view Bag
            ViewBag.users = users;

            return View(taskcategories);
            //var taskCategory = _context.TaskCategories.Find(id);
        }

        // POST: TaskCategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <ActionResult> Edit( TaskCategory taskCategory)
        {
            _context.TaskCategories.Update(taskCategory);
            await _context.SaveChangesAsync();
            //return View();
            return RedirectToAction("ViewTaskCategory");

        }
        // GET: TaskCategoryController/Details/5

        public IActionResult Details(int id)
        {
            var categoryList = _context.TaskCategories.Find(id);
            var taskCategories = _context.TaskCategories.ToList();
            foreach(var item in taskCategories)
            {
                var managers = _context.Users.Find(item.ManagerId);
                item.Manager = managers;
            }
            //return View(categoryList);
            return PartialView("_detailTaskCategory",categoryList);


        }

        // GET: TaskCategoryController/Delete/5
        // GET: Movies/Delete/5

        //public ActionResult Delete(int id)
        //{
        //    var taskCategory1 = _context.TaskCategories.Find(id);

        //    var taskCatogories = _context.TaskCategories.ToList();

        //    foreach (var item in taskCatogories)
        //    {
        //        var managers = _context.Users.Find(item.ManagerId);
        //        item.Manager = managers;
        //    }
        //    return View(taskCategory1);
        //}


        [Authorize(Roles = "Admin")]
        // POST: TaskCategoryController/Delete/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                var taskCategory = _context.TaskCategories.Where(a => a.Id == id).FirstOrDefault();
                _context.TaskCategories.Remove(taskCategory);
                _context.SaveChanges();
                //TempData["Success"] = "Cateogry deleted successfully";
                return Json(true);
            }
            catch
            {
                //ViewBag.ErrorMessage = "This category is linked with task hence cannot be deleted";
                return Json(false);
            }


        }

    }
}
