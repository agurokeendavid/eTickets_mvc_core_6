using eTickets.Data.Services;
using eTickets.Models;
using eTickets.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eTickets.Controllers;

public class MoviesController : Controller
{
    private readonly IMoviesService _service;

    public MoviesController(IMoviesService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index()
    {
        IEnumerable<Movie> allMovies = await _service.GetAllAsync(n => n.Cinema);
        return View(allMovies);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Filter(string searchString)
    {
        var allMovies = await _service.GetAllAsync(n => n.Cinema);

        if (!string.IsNullOrWhiteSpace(searchString))
        {
            var filteredResult = allMovies.Where(n => n.Name.Contains(searchString) || n.Description.Contains(searchString)).ToList();
            return View("Index", filteredResult);
        }
        
        return View("Index", allMovies);
    }
    
    //GET: Movies/Details/1
    public async Task<IActionResult> Details(int id)
    {
        var movieDetail = await _service.GetMovieByIdAsync(id);
        return View(movieDetail);
    }
    
    // Get: Movies/create
    public async Task<IActionResult> Create()
    {
        var movieDropdownsData = await _service.GetNewMovieDropdownsValuesAsync();
        ViewBag.Cinemas = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
        ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "Id", "FullName");
        ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "FullName");
        return View();
    } 
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(NewMovieViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            var movieDropdownsData = await _service.GetNewMovieDropdownsValuesAsync();
            ViewBag.Cinemas = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "Id", "FullName");
            ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "FullName");
            return View(viewModel);
        }

        await _service.AddNewMovieAsync(viewModel);
        return RedirectToAction(nameof(Index));
    }
    
    // Get: Movies/edit/1
    public async Task<IActionResult> Edit(int id)
    {
        var movieDetails = await _service.GetMovieByIdAsync(id);
        if (movieDetails is null)
            return View("NotFound");

        var viewModel = new NewMovieViewModel()
        {
            Id = movieDetails.Id,
            Name = movieDetails.Name,
            CinemaId = movieDetails.CinemaId,
            StartDate = movieDetails.StartDate,
            EndDate = movieDetails.EndDate,
            Description = movieDetails.Description,
            Price = movieDetails.Price,
            ImageURL = movieDetails.ImageURL,
            MovieCategory = movieDetails.MovieCategory,
            ProducerId = movieDetails.ProducerId,
            ActorIds = movieDetails.Actors_Movies.Select(am => am.ActorId).ToList()
        };
        
        var movieDropdownsData = await _service.GetNewMovieDropdownsValuesAsync();
        ViewBag.Cinemas = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
        ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "Id", "FullName");
        ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "FullName");
        return View(viewModel);
    }

    [ValidateAntiForgeryToken]
    [HttpPost]
    public async Task<IActionResult> Edit(int id, NewMovieViewModel viewModel)
    {
        if (id != viewModel.Id)
            return View("NotFound");
        
        if (!ModelState.IsValid)
        {
            var movieDropdownsData = await _service.GetNewMovieDropdownsValuesAsync();
            ViewBag.Cinemas = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "Id", "FullName");
            ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "FullName");
            return View(viewModel);
        }

        await _service.UpdateMovieAsync(viewModel);
        return RedirectToAction(nameof(Index));
    }
}