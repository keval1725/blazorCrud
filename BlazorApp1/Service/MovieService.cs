using BlazorApp1.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp1.Service
{
    public class MovieService: IMovieService
    {
        private readonly ApplicationDbContext _db;
        private readonly HttpClient _httpClient;

        public MovieService(ApplicationDbContext db, HttpClient httpClient)
        {
            _db = db;
            _httpClient = httpClient;
        }
        public async Task<Movie> GetMovieById(int Id)
        {
            var movies =await _db.Movie.Where(i => i.Id == Id && i.IsDeleted == false || i.IsDeleted == null).FirstOrDefaultAsync();
            if (movies != null)
            {
                return movies;
            }
            else
            {
                return null;

            }
        }
        public  async Task<bool> CreateUpdateMovies(Movie movie)
        {
            if (movie.Id == 0)
            {
               
                 await _db.Movie.AddAsync(movie);
                await _db.SaveChangesAsync();
                return true;
            }
            else
            {
                var Movies =  _db.Movie.Where(i => i.Id == movie.Id && i.IsDeleted == false || i.IsDeleted == null).FirstOrDefault();
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
                    return true;
                }
                else
                {
                    return false;
                }
                
            }
        }
        public async Task<List<Movie>> GetMovies()
        {
            var movies = await _httpClient.GetAsync<List<Movie>>("api/Movies/GetMovies");
            //var movies = await _db.Movie.Where(i => i.IsDeleted == false || i.IsDeleted == null).ToListAsync();

            return movies;
        }
        public async Task<Movie> DeleteMovieById(int Id)
        {
            var movies = await _db.Movie.Where(i => i.Id == Id && i.IsDeleted == false || i.IsDeleted == null).FirstOrDefaultAsync();
            if (movies != null)
            {
                 _db.Movie.Remove(movies);
                await _db.SaveChangesAsync();
                return movies;
            }
            else
            {
                return null;

            }
        }
    }
}
