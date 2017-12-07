using rznuProjektApi.Models;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace rznuProjektApi.Controllers
{
    [RoutePrefix("api/Movie")]
    [Authorize]
    public class MovieController : ApiController
    {
        public readonly MovieContext _context;

        public MovieController()
        {
            _context = new MovieContext();
        }

        [Route("")]
        [HttpGet]
        public IHttpActionResult GetMovies()
        {
            var movies = _context.Movies.Include(m => m.MovieActors).Select(m => new MovieDto()
            {
                Title = m.Title,
                Category = m.Category,
                PremiereDate = m.PremiereDate,
                Actors = m.MovieActors
            }).ToList();

            if (movies == null)
                return NotFound();

            return Ok(movies);
        }

        [Route("{id:int}", Name = "GetMovieById")]
        [HttpGet]
        public IHttpActionResult GetMovie(int id)
        {
            var movie = _context.Movies.Include(m => m.MovieActors).ToList().Find(m => m.Id == id);

            if (movie == null)
                return NotFound();


            return Ok(movie);
        }

        [Route("")]
        [HttpPost]
        //[ResponseType(typeof(MovieDto))]
        public IHttpActionResult PostMovie([FromBody] MovieDto movieDto)
        {
            var movie = new Movie()
            {
                Title = movieDto.Title,
                Category = movieDto.Category,
                PremiereDate = movieDto.PremiereDate,
                MovieActors = movieDto.Actors
            };

            if (movie != null)
            {
                _context.Movies.Add(movie);
                _context.SaveChanges();
            }

            //var response = Request.CreateResponse(HttpStatusCode.Created);
            //var uri = Url.Link("GetMovieById", new { id = movie.Id });
            //response.Headers.Location = new Uri(uri);
            //return response; HttpResponseMessage

            return CreatedAtRoute("GetMovieById", new { id = movie.Id }, movie);
        }

        [Route("{id:int}")]
        [HttpPut]
        public IHttpActionResult PutMovie([FromBody] MovieDto movieDto, int id)
        {
            var movie = _context.Movies.ToList().Find(m => m.Id == id);

            movie.Title = movieDto.Title;
            movie.Category = movieDto.Category;
            movie.PremiereDate = movieDto.PremiereDate;
           
            foreach (var item in movieDto.Actors)
            {
                movie.MovieActors.Add(item);
            }

            _context.SaveChanges();

            return Content(HttpStatusCode.Accepted, movie);
        }

        [Route("{id:int}")]
        [HttpDelete]
        public IHttpActionResult DeleteMovie(int id)
        {
            var movie = _context.Movies.ToList().Find(m => m.Id == id);

            _context.Movies.Remove(movie);
            _context.SaveChanges();

            return Ok();
        }

        [Route("{id:int}/actors")]
        [HttpGet]
        public IHttpActionResult GetMovieActors(int id)
        {
            var movieActors = _context.Movies.Include(m => m.MovieActors).ToList().Find(m => m.Id == id).MovieActors;

            if (movieActors == null)
                return NotFound();

            return Ok(movieActors);
        }

    }
}
