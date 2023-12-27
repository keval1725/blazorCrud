using BlazorApp1.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext _db;

        public MoviesController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUpdateMovies(Movie movie, int? Id)
        {
            if (Id == 0)
            {
                Movie movies = new Movie();
                movies = movie;
                await _db.Movie.AddAsync(movies);
                await _db.SaveChangesAsync();
                return Ok(movies);
            }
            else
            {
                var Movies = await _db.Movie.Where(i => i.Id == Id && i.IsDeleted == false || i.IsDeleted == null).FirstOrDefaultAsync();
                if (Movies != null)
                {
                    Movies.Title = movie.Title;
                    Movies.Director = movie.Director;
                    Movies.Genre = movie.Genre;
                    Movies.ReleaseYear = movie.ReleaseYear;
                    Movies.Rating = movie.Rating;
                    Movies.Duration = movie.Duration;
                    Movies.UpdatedAt = movie.UpdatedAt;
                    await _db.SaveChangesAsync();
                    return Ok(Movies);
                }
                else
                {
                    return BadRequest($"Movies Not Found ");
                }
            }
        }

        [HttpGet]
        public async Task<List<Movie>> GetMovies()
        {
            var movies = await _db.Movie.Where(i => i.IsDeleted == false || i.IsDeleted == null).ToListAsync();

            return await Task.FromResult(movies);
        }
        [HttpGet]
        public IActionResult GetMovieById(int Id)
        {
            var movies = _db.Movie.Where(i => i.Id == Id && i.IsDeleted == false || i.IsDeleted == null).FirstOrDefaultAsync();
            if (movies != null)
            {
                return Ok(movies);
            }
            else
            {
                return NotFound();

            }
        }
        public async Task<IActionResult> DeleteMovieById(int Id)
        {
            var movies = await _db.Movie.Where(i => i.Id == Id && i.IsDeleted == false || i.IsDeleted == null).FirstOrDefaultAsync();
            if (movies != null)
            {
                movies.IsDeleted = true;
                await _db.SaveChangesAsync();
               return Ok("Movie Delete SuccessFully..");
            }
            else
            {
              return  NotFound("Movie Not Found");
            }
        }
    }
}
