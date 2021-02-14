using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Viajemos.Web.Data.Entities
{
    public class Libro
    {
        public int Id { get; set; }

        [MaxLength(20, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Titulo { get; set; }

        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Sinopsis { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public int Paginas { get; set; }

        public Editorial Editorial  { get; set; }

        public Autor Autor { get; set; }

        public ICollection<ImagenLibro> ImagenLibros { get; set; }

    }
}
