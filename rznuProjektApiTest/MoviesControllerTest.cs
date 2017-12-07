using Microsoft.VisualStudio.TestTools.UnitTesting;
using rznuProjektApi.Controllers;
using System.Linq;
using rznuProjektApi.Models;
using System.Web.Http.Results;
using System.Collections.Generic;
using System.Web.Http;
using System;
using System.Net;

namespace rznuProjektApiTest
{
    [TestClass]
    public class MoviesControllerTest
    {
        [TestMethod]
        public void TestGetMoviesReturnsOk()
        {
            var controller = new MovieController();

            var result = controller.GetMovies() as OkNegotiatedContentResult<List<MovieDto>>;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
        }

        [TestMethod]
        public void TestGetMovieReturnsOk()
        {
            var controller = new MovieController();

            var movie = controller._context.Movies.ToList().ElementAtOrDefault(5);
            
            var result = controller.GetMovie(movie.Id) as OkNegotiatedContentResult<Movie>;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
            Assert.AreEqual(movie.Id, result.Content.Id);
        }

        [TestMethod]
        public void TestGetMovieReturnsNotFound()
        {
            var controller = new MovieController();

            var result = controller.GetMovie(100);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void DeleteReturnsOk()
        {
           
            var controller = new MovieController();

            var movie = controller._context.Movies.ToList().ElementAtOrDefault(5);

            var actionResult = controller.DeleteMovie(movie.Id);
            
            Assert.IsInstanceOfType(actionResult, typeof(OkResult));
        }

        [TestMethod]
        public void PostMethodSetsLocationHeader()
        {
            
            var controller = new MovieController();

            var actionResult = controller.PostMovie(new MovieDto { Title = "title", Category = "bla", PremiereDate = DateTime.Now });
            var createdResult = actionResult as CreatedAtRouteNegotiatedContentResult<Movie>;
            
            Assert.IsNotNull(createdResult);
            Assert.AreEqual("GetMovieById", createdResult.RouteName);
        }

        [TestMethod]
        public void PutReturnsContentResult()
        {
            var controller = new MovieController();

            var movie = controller._context.Movies.ToList().ElementAtOrDefault(5);
            
            var actionResult = controller.PutMovie(new MovieDto { Title = "bla", Category = "categ", PremiereDate = DateTime.Now, Actors = movie.MovieActors }, movie.Id);
            var contentResult = actionResult as NegotiatedContentResult<Movie>;

            Assert.IsNotNull(contentResult);
            Assert.AreEqual(HttpStatusCode.Accepted, contentResult.StatusCode);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(movie.Id, contentResult.Content.Id);
        }

        [TestMethod]
        public void TestGetMoviesActorsReturnsOk()
        {
            var controller = new MovieController();

            var movie = controller._context.Movies.ToList().ElementAtOrDefault(5);
            
            var result = controller.GetMovieActors(movie.Id) as OkNegotiatedContentResult<List<Actor>>;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
        }
    }
}
