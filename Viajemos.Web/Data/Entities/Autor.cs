﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Viajemos.Web.Data.Entities
{
    public class Autor
    {
        public int Id { get; set; }

        public User User { get; set; }
        public ICollection<Libro> Libros  { get; set; }

    }
}
