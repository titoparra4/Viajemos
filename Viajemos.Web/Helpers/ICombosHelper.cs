using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Viajemos.Web.Helpers
{
    public interface ICombosHelper
    {
        IEnumerable<SelectListItem> GetComboEditorials();

    }
}