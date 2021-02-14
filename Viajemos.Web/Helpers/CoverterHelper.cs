using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Viajemos.Web.Data;
using Viajemos.Web.Data.Entities;
using Viajemos.Web.Models;

namespace Viajemos.Web.Helpers
{
    public class CoverterHelper : ICoverterHelper
    {
        private readonly DataContext _dataContext;

        public CoverterHelper(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<Libro> ToLibroAsync(LibroViewModel model, bool isNew)
        {
            return new Libro
            {
                Titulo = model.Titulo,
                Sinopsis = model.Sinopsis,
                Paginas = model.Paginas,
                Id = isNew ? 0 : model.Id,
                Autor = await _dataContext.Autors.FindAsync(model.AutorId),
                ImagenLibros = isNew ? new List<ImagenLibro>() : model.ImagenLibros,
                Editorial = await _dataContext.Editorials.FindAsync(model.EditorialId),
            };
        }
    }
}
