using eTickets.Data.Base;
using eTickets.Models;
using eTickets.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Data.Services;

public class MoviesService : EntityBaseRepository<Movie>, IMoviesService
{
    private readonly AppDbContext _context;
    public MoviesService(AppDbContext dbContext) : base(dbContext)
    {
        _context = dbContext;
    }

    public async Task<Movie> GetMovieByIdAsync(int id)
    {
        var movieDetails = await _context.Movies
            .Include(c => c.Cinema)
            .Include(p => p.Producer)
            .Include(am => am.Actors_Movies).ThenInclude(a => a.Actor)
            .FirstOrDefaultAsync(n => n.Id == id);
        return movieDetails;
    }

    public async Task<NewMovieDropdownsViewModel> GetNewMovieDropdownsValuesAsync()
    {
        var response = new NewMovieDropdownsViewModel
        {
            Actors = await _context.Actors.OrderBy(n => n.FullName).ToListAsync(),
            Cinemas = await _context.Cinemas.OrderBy(c => c.Name).ToListAsync(),
            Producers = await _context.Producers.OrderBy(p => p.FullName).ToListAsync()
        };

        return response;
    }
    
    public async Task AddNewMovieAsync(NewMovieViewModel viewModel)
    {
        var newMovie = new Movie()
        {
            Name = viewModel.Name,
            Description = viewModel.Description,
            Price = viewModel.Price,
            ImageURL = viewModel.ImageURL,
            CinemaId = viewModel.CinemaId,
            StartDate = viewModel.StartDate,
            EndDate = viewModel.EndDate,
            MovieCategory = viewModel.MovieCategory,
            ProducerId = viewModel.ProducerId
        };

        await _context.Movies.AddAsync(newMovie);
        await _context.SaveChangesAsync();
        
        // Add Movie Actors
        foreach (var actorId in viewModel.ActorIds)
        {
            var newActorMovie = new Actor_Movie()
            {
                MovieId = newMovie.Id,
                ActorId = actorId
            };
            await _context.Actors_Movies.AddAsync(newActorMovie);
        }
        
        await _context.SaveChangesAsync();
    }

    public async Task UpdateMovieAsync(NewMovieViewModel viewModel)
    {
        var dbMovie = await _context.Movies.FirstOrDefaultAsync(n => n.Id == viewModel.Id);

        if (dbMovie != null)
        {
            dbMovie.Name = viewModel.Name;
            dbMovie.Description = viewModel.Description;
            dbMovie.Price = viewModel.Price;
            dbMovie.ImageURL = viewModel.ImageURL;
            dbMovie.CinemaId = viewModel.CinemaId;
            dbMovie.StartDate = viewModel.StartDate;
            dbMovie.EndDate = viewModel.EndDate;
            dbMovie.MovieCategory = viewModel.MovieCategory;
            dbMovie.ProducerId = viewModel.ProducerId;
            _context.Update(dbMovie);
            await _context.SaveChangesAsync();
        }
        
        // Remove existing actors
        var existingActorsDb = await _context.Actors_Movies.Where(n => n.MovieId == viewModel.Id).ToListAsync();
        _context.Actors_Movies.RemoveRange(existingActorsDb);
        await _context.SaveChangesAsync();
        
        // Add Movie Actors
        foreach (var actorId in viewModel.ActorIds)
        {
            var newActorMovie = new Actor_Movie()
            {
                MovieId = viewModel.Id,
                ActorId = actorId
            };
            await _context.Actors_Movies.AddAsync(newActorMovie);
        }
        
        await _context.SaveChangesAsync();
    }
}