using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Viajemos.Web.Data;

namespace Viajemos.Web.Helpers
{
    public class CombosHelper : ICombosHelper
    {
        private readonly DataContext _dataContext;

        public CombosHelper(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public IEnumerable<SelectListItem> GetComboEditorials()
        {
            var list = _dataContext.Editorials.Select(p => new SelectListItem
            {
                Text = p.Name,
                Value = $"{p.Id}"
            }).OrderBy(p => p.Text).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "(Seleccione una Editorial...)",
                Value = "0"
            });

            return list;

        }
    }
}
