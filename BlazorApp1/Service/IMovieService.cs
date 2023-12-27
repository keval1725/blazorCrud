using BlazorApp1.Data;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp1.Service
{
    public interface IMovieService
    {
        Task<bool> CreateUpdateMovies(Movie movie);
        Task<Movie> GetMovieById(int Id);
        Task<List<Movie>> GetMovies();
        Task<Movie> DeleteMovieById(int Id);
    }
}
