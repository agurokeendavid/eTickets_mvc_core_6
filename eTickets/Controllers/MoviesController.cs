using eTickets.Data.Services;
using eTickets.Models;
using eTickets.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace eTickets.Controllers;

public class MoviesController : Controller
{
    private readonly IMoviesService _service;

    public MoviesController(IMoviesService service)
    {
        _service = service;
    }

    public async Task<IActionResult>  Index()
    {
        IEnumerable<Movie> allMovies = await _service.GetAllAsync(n => n.Cinema);
        return View(allMovies);
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
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(NewMovieViewModel viewModel)
    {
        return View(viewModel);
    }
}