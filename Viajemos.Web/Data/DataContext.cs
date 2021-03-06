﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Viajemos.Web.Data.Entities;

namespace Viajemos.Web.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<Autor> Autors { get; set; }

        public DbSet<Editorial> Editorials { get; set; }

        public DbSet<ImagenLibro> ImagenLibros { get; set; }

        public DbSet<Libro> Libros { get; set; }

        public DbSet<Manager> Managers { get; set; }

    }
}
