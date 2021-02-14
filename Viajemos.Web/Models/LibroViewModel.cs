using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Viajemos.Web.Data.Entities;

namespace Viajemos.Web.Models
{
    public class LibroViewModel : Libro
    {
        public int AutorId { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "Editorial")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una editorial.")]
        public int EditorialId { get; set; }

        public IEnumerable<SelectListItem> Editorials { get; set; }

    }
}
