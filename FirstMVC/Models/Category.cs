using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FirstMVC.Models
{
	public class Category
	{

		public int Id { get; set; }
		[Display(Name="Kategori Adı")]
		[Required]
		[MaxLength(50)]
		public string Name { get; set; }
		[Required]
		[MaxLength(50)]
		[Display(Name = "Tanım")]
		[DataType(DataType.MultilineText)]
		public string Description { get; set; }
		public virtual ICollection<Project> Projects { get; set; }
	}
}