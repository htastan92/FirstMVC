using FirstMVC.Data;
using FirstMVC.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
		public ActionResult Create(Project project, HttpPostedFileBase upload)
		{
			if (ModelState.IsValid)
			{

				using (var db = new ApplicationDbContext())
				{
					try
					{
						project.Photo = UploadFile(upload);
					}
					catch (Exception ex)
					{

						ViewBag.Error = ex.Message;
						ViewBag.Categories = new SelectList(db.Categories.ToList(), "Id", "Name");
						return View(project);
					}

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

		public string UploadFile(HttpPostedFileBase upload)
		{
			if (upload != null && upload.ContentLength > 0)
			{
				var extension = Path.GetExtension(upload.FileName).ToLower();
				if (extension == ".jpg" || extension == ".jpeg" || extension == ".gif" || extension == ".png")
				{
					if (Directory.Exists(Server.MapPath("~/Uploads")))
					{
						string fileName = upload.FileName.ToLower();
						fileName = fileName.Replace("İ", "i");
						fileName = fileName.Replace("Ş", "s");
						fileName = fileName.Replace("ı", "i");
						fileName = fileName.Replace("(", "");
						fileName = fileName.Replace("Ğ", "G");
						fileName = fileName.Replace("ğ", "g");
						fileName = fileName.Replace(" ", "-");
						fileName = fileName.Replace(",", "");
						fileName = fileName.Replace("ö", "o");
						fileName = fileName.Replace("ü", "u");
						fileName = fileName.Replace("`", "");

						fileName = DateTime.Now.Ticks.ToString() + fileName;
						upload.SaveAs(Path.Combine(Server.MapPath("~/Uploads"), fileName));
						return fileName;
					}
					else
					{
						throw new Exception("Uploads dizini mevcut değil");
					}
				}
				else
				{
					throw new Exception("Dosya türü .jpg , .jpeg , .gif ya da .png olmalıdır ");
				}
			}
			return null;
		}
		public ActionResult Edit(int id)
		{
			using (var db = new ApplicationDbContext())
			{
				var project = db.Projects.Where(x => x.Id == id).FirstOrDefault();
				if (project != null)
				{
					ViewBag.Categories = new SelectList(db.Categories.ToList(), "Id", "Name");
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
		public ActionResult Edit(Project project, HttpPostedFileBase upload, string sil)
		{
			if (ModelState.IsValid)
			{
				using (var db = new ApplicationDbContext())
				{
					try
					{
						project.Photo = UploadFile(upload);
					}
					catch (Exception ex)
					{

						ViewBag.Error = ex.Message;
						ViewBag.Categories = new SelectList(db.Categories.ToList(), "Id", "Name");
						return View(project);
					}

					var dbproject = db.Projects.Where(p => p.Id == project.Id).FirstOrDefault();
					dbproject.Title = project.Title;
					dbproject.Description = project.Description;
					dbproject.CategoryId = project.CategoryId;
					dbproject.Body = project.Body;
					if (!string.IsNullOrEmpty(project.Photo))
					{
						dbproject.Photo = project.Photo;
						
					}
					if (!string.IsNullOrEmpty(sil))
					{
						dbproject.Photo = null;
					}
					db.SaveChanges();
				}

			}
			using (var db = new ApplicationDbContext())
			{
				ViewBag.Categories = new SelectList(db.Categories.ToList(), "Id", "Name");

			}
			return RedirectToAction("Index");
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