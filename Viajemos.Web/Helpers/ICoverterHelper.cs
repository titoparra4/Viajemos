using System.Threading.Tasks;
using Viajemos.Web.Data.Entities;
using Viajemos.Web.Models;

namespace Viajemos.Web.Helpers
{
    public interface ICoverterHelper
    {
        Task<Libro> ToLibroAsync(LibroViewModel model, bool isNew);
        LibroViewModel ToLibroViewModel(Libro libro);
    }
}