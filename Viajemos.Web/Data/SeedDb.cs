using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Viajemos.Web.Data.Entities;

namespace Viajemos.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _dataContext;

        public SeedDb(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task SeedAsync()
        {
            await _dataContext.Database.EnsureCreatedAsync();
            await CheckEditorialsAsync();
            await CheckAutorsAsync();
            await CheckLibrosAsync();
        }

        private async Task CheckEditorialsAsync()
        {
            if (!_dataContext.Editorials.Any())
            {
                _dataContext.Editorials.Add(new Entities.Editorial { Name = "Nombre", Sede = "Bogota" });
                await _dataContext.SaveChangesAsync();
            }
        }
        private async Task CheckLibrosAsync()
        {
            var autor = _dataContext.Autors.FirstOrDefault();
            var editorial = _dataContext.Editorials.FirstOrDefault();
            if (!_dataContext.Libros.Any())
            {
                AddLibro("Net Core", "Es un sucesor multiplataforma de .NET Framework.", autor, editorial, 100);
                await _dataContext.SaveChangesAsync();
            }
        }

        private void AddLibro(
                                string titulo,
                                string sinopsis,
                                Autor autor,
                                Editorial editorial,
                                int paginas)
            {
                _dataContext.Libros.Add(new Libro
                {
                    Titulo = titulo,
                    Sinopsis = sinopsis,
                    Autor = autor,
                    Editorial = editorial,
                    Paginas = paginas
                });
        }

        private async Task CheckAutorsAsync()
        {
            if (!_dataContext.Autors.Any())
            {
                AddAutor("Tito", "Parra");
                await _dataContext.SaveChangesAsync();
            }
        }

        private void AddAutor(
            string nombre,
            string apellido)
        {
            _dataContext.Autors.Add(new Autor
            {
                Nombre = nombre,
                Apellido = apellido,
            });
        }
    }

}
