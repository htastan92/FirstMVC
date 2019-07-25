﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FirstMVC.Models
{
	public class Project
	{
		public int Id { get; set; }
		[Required]
		[MaxLength(100)]
		[Display(Name ="Başlık")]
		public string Title { get; set; }
		[Required]
		[MaxLength(50)]
		[Display(Name = "Tanım")]
		[DataType(DataType.MultilineText)]
		public string Description{ get; set; }
		[DataType(DataType.Html)]
		[Display(Name ="Açıklama")]

		public string Body { get; set; }
		[Display(Name = "Fotoğraf")]
		public string Photo { get; set; }
		public int? CategoryId { get; set; }
		[ForeignKey("CategoryId")]
		public virtual Category Category { get; set; }


	}
}