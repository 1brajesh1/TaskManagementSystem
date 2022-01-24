using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

namespace TaskManagementSystem.Controllers
{
    [Authorize]
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

       
        public IActionResult Create(TaskCategory taskcategory)
        {
            _context.TaskCategories.Add(taskcategory);
            _context.SaveChanges();
            //return View();
            return RedirectToAction("ViewTaskCategory");
        }

        public IActionResult ViewTaskCategory()
        {
            var taskcategory = _context.TaskCategories.ToList();

            foreach(var item in taskcategory)
            {
                var managers = _context.Users.Find(item.ManagerId);
                item.Manager = managers;
            }

            return View(taskcategory);

        }


        // GET: TaskCategoryController/Edit/5
        public ActionResult Edit(int id)

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

            
            var taskCategory = _context.TaskCategories.Find(id);
            return View(taskCategory);
        }

        // POST: TaskCategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <ActionResult> Edit(int id, TaskCategory taskCategory)
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
            return View(categoryList);

        }

        // GET: TaskCategoryController/Delete/5
        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskdelete1 = await _context.TaskCategories
                .FirstOrDefaultAsync(TaskCategory => TaskCategory.Id == id);
            if (taskdelete1 == null)
            {
                return NotFound();
            }

            return View(taskdelete1);
        }
            
        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var taskdelete1 = await _context.TaskCategories.FindAsync(id);
            _context.TaskCategories.Remove(taskdelete1);
            await _context.SaveChangesAsync();
            return RedirectToAction("ViewTaskCategory");
        }
    }
}
