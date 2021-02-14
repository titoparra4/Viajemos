using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Viajemos.Web.Data.Entities
{
    public class ImagenLibro
    {
        public int Id { get; set; }

        [Display(Name = "Imagen")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string ImageUrl { get; set; }

        public Libro Libro  { get; set; }

        // TODO: Change the path when publish
        public string ImageFullPath => $"https://viajemos.azurewebsites.net{ImageUrl.Substring(1)}";

    }
}
