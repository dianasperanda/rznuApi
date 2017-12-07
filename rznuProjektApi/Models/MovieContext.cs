using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace rznuProjektApi.Models
{
    public class MovieContext : DbContext
    {
        public MovieContext() : base("name=MovieContext")
        {
        }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<Actor> Actors { get; set; }
    }
}