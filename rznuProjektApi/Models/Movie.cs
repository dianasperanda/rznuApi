using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace rznuProjektApi.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Category { get; set; }

        public DateTime PremiereDate { get; set; }

        public List<Actor> MovieActors { get; set; } = new List<Actor>();
    }
}