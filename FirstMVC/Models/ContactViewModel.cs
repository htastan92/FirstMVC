using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FirstMVC.Models
{
	public class ContactViewModel
	{
		[Display(Name="Ad")]
		[MaxLength(50)]
		[Required]
		public string FirstName { get; set; }
		[Display(Name = "Soyad")]
		[MaxLength(50)]
		[Required]
		public string LastName { get; set; }
		[Display(Name="Telefon")]
		[MaxLength(20)]
		[Required]
		[Phone]
		public string Phone { get; set; }
		[Display(Name ="Eposta")]
		[MaxLength(100)]
		[EmailAddress]
		[Required]
		public string Email { get; set; }
		[Display(Name ="Mesaj")]
		[MaxLength(4000)]
		[Required]
		public string Message { get; set; }
		[Display(Name ="Kvkk")]
		[Required]
		public bool PrivacyPolicyAccepted { get; set; }
	}
}