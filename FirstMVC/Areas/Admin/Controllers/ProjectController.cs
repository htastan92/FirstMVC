using FirstMVC.Data;
using FirstMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FirstMVC.Areas.Admin.Controllers
{
	[RouteArea("Admin")]
	public class ProjectController : Controller
	{
		// GET: Admin/Project
		public ActionResult Index()
		{
			using (var db = new ApplicationDbContext())
			{
				var projects = db.Projects.Include("Category").ToList();

				return View(projects);
			}

		}

		public ActionResult Details()
		{
			using (var db = new ApplicationDbContext())
			{
				var projects = db.Projects.ToList();
				return View(projects);

			}
		}
		public ActionResult Create()
		{
			var project = new Project();
			using (var db = new ApplicationDbContext())
			{
				ViewBag.Categories = new SelectList(db.Categories.ToList(), "Id", "Name");
				
			}
			return View(project);
		}
		[HttpPost]
		[ValidateInput(false)]
		public ActionResult Create(Project project)
		{
			if (ModelState.IsValid)
			{
				using (var db = new ApplicationDbContext())
				{
					db.Projects.Add(project);
					db.SaveChanges();
					return RedirectToAction("Index");

				}
			}
			using (var db = new ApplicationDbContext())
			{
				ViewBag.Categories = new SelectList(db.Categories.ToList(), "Id", "Name");

			}
			return View(project);
		}

		public ActionResult Edit(int id)
		{
			using (var db = new ApplicationDbContext())
			{
				var project = db.Projects.Where(x => x.Id == id).FirstOrDefault();
				if (project != null)
				{
					return View(project);
				}
				else
				{
					return HttpNotFound();
				}
			}
		}
		[HttpPost]
		[ValidateInput(false)]
		public ActionResult Edit(Project project)
		{
			if (ModelState.IsValid)
			{
				using (var db = new ApplicationDbContext())
				{
					var dbproject = db.Projects.Where(p => p.Id == project.Id).FirstOrDefault();
					dbproject.Title = project.Title;
					dbproject.Description = project.Description;
					dbproject.Category.Name = project.Category.Name;
					dbproject.Body = project.Body;
					dbproject.Photo = project.Photo;
					db.SaveChanges();
				}

			}
			using (var db = new ApplicationDbContext())
			{
				ViewBag.Categories = new SelectList(db.Categories.ToList(), "Id", "Name");

			}
			return View(project);
		}

		public ActionResult Delete(int id)
		{
			using (var db = new ApplicationDbContext())
			{
				var project = db.Projects.FirstOrDefault(x => x.Id == id);
				if (project != null)
				{
					db.Projects.Remove(project);
					db.SaveChanges();
					return RedirectToAction("Index");
				}
				else
				{
					return HttpNotFound();
				}
			}

		}


	}
}