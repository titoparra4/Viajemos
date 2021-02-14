using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Viajemos.Web.Data.Entities;

namespace Viajemos.Web.Models
{
	public class LibroImageViewModel : ImagenLibro
	{
		[Display(Name = "Image")]
		public IFormFile ImageFile { get; set; }
	}

}
