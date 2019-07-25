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
	public class CategoryController : Controller
	{
		// GET: Admin/Category
		public ActionResult Index()
		{
			using (var db = new ApplicationDbContext())
			{

				var categories = db.Categories.ToList();
				return View(categories);
			}

		}

		public ActionResult Create()
		{
			var project = new Project();
			return View();
		}
		[HttpPost]
		public ActionResult Create(Category category)
		{
			using (var db = new ApplicationDbContext())
			{
				if (ModelState.IsValid)
				{
					db.Categories.Add(category);
					db.SaveChanges();
					return RedirectToAction("Index");
				}
			}
			return View(category);
		}
		public ActionResult Edit(int id)
		{
			using (var db = new ApplicationDbContext())
			{
				var category = db.Categories.FirstOrDefault(x => x.Id == id);
				if (category != null)
				{
					return View(category);
				}
				else
				{
					return HttpNotFound();
				}
			}
		}
		[HttpPost]
		[ValidateInput(false)]
		public ActionResult Edit(Category category)
		{
			if (ModelState.IsValid)
			{
				using (var db = new ApplicationDbContext())
				{
					var oldcategory = db.Categories.Where(x => x.Id == category.Id).FirstOrDefault();
					oldcategory.Name = category.Name;
					oldcategory.Description = category.Description;
					db.SaveChanges();
				}
			}
			return View(category);
		}

		public ActionResult Delete (int id)
		{
			using (var db = new ApplicationDbContext())
			{
				var category = db.Categories.FirstOrDefault(x => x.Id == id);
				var projects = db.Projects.Where(x => x.CategoryId == id);
				
				if (category != null)
				{
					foreach (var item in projects)
					{
						item.CategoryId = null;
					}
					db.Categories.Remove(category);
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