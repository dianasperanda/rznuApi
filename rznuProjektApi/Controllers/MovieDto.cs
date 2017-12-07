using System;
using System.Collections.Generic;
using rznuProjektApi.Models;

namespace rznuProjektApi.Controllers
{
    public class MovieDto
    {
        public List<Actor> Actors { get; set; }
        public string Category { get; set; }
        public DateTime PremiereDate { get; set; }
        public string Title { get; set; }
    }
}