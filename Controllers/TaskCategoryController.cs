using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagementSystem.Data;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Controllers
{
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

        // GET: TaskCategoryController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TaskCategoryController/Create
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
        //[ValidateAntiForgeryToken]

       
        public IActionResult Create(TaskCategory taskcategory)
        {
            _context.TaskCategories.Add(taskcategory);
            _context.SaveChanges();
            //return View();
            return RedirectToAction("TaskCategoryAction");
        }

        public IActionResult TaskCategoryAction()
        {
            var taskcategory = _context.TaskCategories.ToList();
            return View(taskcategory);

        }


        // GET: TaskCategoryController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TaskCategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TaskCategoryController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TaskCategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
