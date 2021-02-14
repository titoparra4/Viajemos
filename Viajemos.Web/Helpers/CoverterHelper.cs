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
        private readonly ICombosHelper _combosHelper;

        public CoverterHelper(DataContext dataContext,
            ICombosHelper combosHelper)
        {
            _dataContext = dataContext;
            _combosHelper = combosHelper;
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

        public LibroViewModel ToLibroViewModel(Libro libro)
        {
            return new LibroViewModel
            {
                Titulo = libro.Titulo,
                Sinopsis = libro.Sinopsis,
                Paginas = libro.Paginas,
                Id = libro.Id,
                Autor = libro.Autor,
                ImagenLibros = libro.ImagenLibros,
                Editorial = libro.Editorial,
                AutorId = libro.Autor.Id,
                EditorialId = libro.Editorial.Id,
                Editorials = _combosHelper.GetComboEditorials()
            };
        }
    }
}
