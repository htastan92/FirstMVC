using FirstMVC.Data;
using FirstMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace FirstMVC.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";
			return View();

		}

		public ActionResult Contact()
		{			

			return View();
		}
		[HttpPost]
		public ActionResult Contact(ContactViewModel model)
		{
			
			if (ModelState.IsValid)
			{
				
				try
				{

					System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage();

					mailMessage.From = new System.Net.Mail.MailAddress("htastan3458@gmail.com", "Hüseyin Taştan Blog");
					mailMessage.Subject = "İletişim Formu: " + model.FirstName + " " + model.LastName;

					mailMessage.To.Add(model.Email);

					string body;
					body = "Ad: " + model.FirstName + "<br />";
					body = "Soyad: " + model.LastName + "<br />";
					body += "Telefon: " + model.Phone + "<br />";
					body += "E-posta: " + model.Email + "<br />";
					body += "Mesaj: " + model.Message + "<br />";
					mailMessage.IsBodyHtml = true;
					mailMessage.Body = body;

					System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587);
					smtp.Credentials = new System.Net.NetworkCredential("htastan3458@gmail.com", "");
					smtp.EnableSsl = true;
					smtp.Send(mailMessage);
					ViewBag.Message = "Mesajınız gönderildi. Teşekkür ederiz.";
				}
				catch (Exception ex)
				{
					ViewBag.Error = "Form gönderimi başarısız oldu. Lütfen daha sonra tekrar deneyiniz.";
					
				}
			}
			return View(model);
		}
		
		public ActionResult Project()
		{
			using (var db = new ApplicationDbContext())
			{
				var projects = db.Projects.ToList();
				return View(projects);
			}
		}
		public ActionResult Kvkk()
		{
			return View();
		}

		public ActionResult CookieConsent()
		{
			return View();
		}

		public ActionResult Cemre()
		{
			return View();
		}

		public ActionResult Ekle()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Ekle(Project proje)
		{
			using (var db = new ApplicationDbContext())
			{
				db.Projects.Add(proje);
				db.SaveChanges();
				return View();
			}
		}
	}
}