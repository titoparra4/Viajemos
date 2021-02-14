using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Viajemos.Web.Data;
using Viajemos.Web.Data.Entities;
using Viajemos.Web.Helpers;
using Viajemos.Web.Models;

namespace Viajemos.Web.Controllers
{
    [Authorize(Roles = "Manager")]
    public class AutorsController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IUserHelper _userHelper;
        private readonly ICombosHelper _combosHelper;
        private readonly ICoverterHelper _coverterHelper;

        public AutorsController(DataContext dataContext,
            IUserHelper userHelper,
            ICombosHelper combosHelper,
            ICoverterHelper coverterHelper)
        {
            _dataContext = dataContext;
            _userHelper = userHelper;
            _combosHelper = combosHelper;
            _coverterHelper = coverterHelper;
        }

        // GET: Autors
        public IActionResult Index()
        {
            return View(_dataContext.Autors
                .Include(a => a.User)
                .Include(a => a.Libros));
        }

        // GET: Autors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autor = await _dataContext.Autors
                .Include(a => a.User)
                .Include(a => a.Libros)
                .ThenInclude(a => a.ImagenLibros)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (autor == null)
            {
                return NotFound();
            }

            return View(autor);
        }

        // GET: Autors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Autors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await CreateUserAsync(model);
                if(user != null)
                {
                    var autor = new Autor
                    {
                        Libros = new List<Libro>(),
                        User = user
                    };

                    _dataContext.Autors.Add(autor);
                    await _dataContext.SaveChangesAsync();
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError(string.Empty, "El Autor con este email ya existe");
            }
            return View(model);
        }

        private async Task<User> CreateUserAsync(AddUserViewModel model)
        {
            var user = new User
            { 
                Nombre = model.Nombre,
                Email = model.Username,
                Apellido = model.Apellido,
                PhoneNumber = model.PhoneNumber,
                UserName = model.Username
            };

            var result = await _userHelper.AddUserAsync(user, model.Password);
            if (result.Succeeded)
            {
                user = await _userHelper.GetUserByEmailAsync(model.Username);
                await _userHelper.AddUserToRoleAsync(user, "Autor");
                return user;
            }
            return null;
        }

        // GET: Autors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autor = await _dataContext.Autors.FindAsync(id);
            if (autor == null)
            {
                return NotFound();
            }
            return View(autor);
        }

        // POST: Autors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id")] Autor autor)
        {
            if (id != autor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _dataContext.Update(autor);
                    await _dataContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AutorExists(autor.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(autor);
        }

        // GET: Autors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autor = await _dataContext.Autors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (autor == null)
            {
                return NotFound();
            }

            return View(autor);
        }

        // POST: Autors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var autor = await _dataContext.Autors.FindAsync(id);
            _dataContext.Autors.Remove(autor);
            await _dataContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AutorExists(int id)
        {
            return _dataContext.Autors.Any(e => e.Id == id);
        }

        public async Task<IActionResult> AddLibro(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autor = await _dataContext.Autors.FindAsync(id);
            if (autor == null)
            {
                return NotFound();
            }

            var model = new LibroViewModel
            {
                AutorId = autor.Id,
                Editorials = _combosHelper.GetComboEditorials()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddLibro(LibroViewModel model)
        {
            if(ModelState.IsValid)
            {
                var libro = await _coverterHelper.ToLibroAsync(model, true);
                _dataContext.Libros.Add(libro);
                await _dataContext.SaveChangesAsync();
                return RedirectToAction($"Details/{model.AutorId}");
            }

            return View(model);
        }
    }
}
