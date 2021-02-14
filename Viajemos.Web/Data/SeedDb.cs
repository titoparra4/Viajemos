using System;
using System.Linq;
using System.Threading.Tasks;
using Viajemos.Web.Data;
using Viajemos.Web.Data.Entities;
using Viajemos.Web.Helpers;

namespace MyLeasing.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _dataContext;
        private readonly IUserHelper _userHelper;

        public SeedDb(
            DataContext dataContext,
            IUserHelper userHelper)
        {
            _dataContext = dataContext;
            _userHelper = userHelper;
        }

        public async Task SeedAsync()
        {
            await _dataContext.Database.EnsureCreatedAsync();
            await CheckRoles();
            var manager = await CheckUserAsync("Tito", "Parra", "tito.parra4@hotmail.com", "320 6372606", "Manager");
            var autor = await CheckUserAsync("Bill", "Gates", "bill@yopmail.com", "320 5555555", "Autor");
            await CheckEditorialsAsync();
            await CheckManagerAsync(manager);
            await CheckAuthorsAsync(autor);
            await CheckLibrosAsync();
        }

        
        private async Task CheckManagerAsync(User user)
        {
            if (!_dataContext.Managers.Any())
            {
                _dataContext.Managers.Add(new Manager { User = user });
                await _dataContext.SaveChangesAsync();
            }
        }

        private async Task<User> CheckUserAsync(string nombre, string apellido, string email, string phone, string role)
        {
            var user = await _userHelper.GetUserByEmailAsync(email);
            if (user == null)
            {
                user = new User
                {
                    Nombre = nombre,
                    Apellido = apellido,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                };

                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, role);
            }

            return user;
        }


        private async Task CheckRoles()
        {
            await _userHelper.CheckRoleAsync("Manager");
            await _userHelper.CheckRoleAsync("Autor");
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

        private async Task CheckEditorialsAsync()
        {
            if (!_dataContext.Editorials.Any())
            {
                _dataContext.Editorials.Add(new Editorial { Name = "Norma", Sede = "Bogota" });
                await _dataContext.SaveChangesAsync();
            }
        }

        private async Task CheckAuthorsAsync(User user)
        {
            if (!_dataContext.Autors.Any())
            {
                _dataContext.Autors.Add(new Autor { User = user });
                await _dataContext.SaveChangesAsync();
            }
        }

        private void AddLibro(string titulo, string sinopsis, Autor autor, Editorial editorial, int paginas)
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
    }
}

