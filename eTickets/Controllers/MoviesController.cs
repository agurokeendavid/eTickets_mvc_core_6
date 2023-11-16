﻿using eTickets.Data;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers;

public class MoviesController : Controller
{
    private readonly AppDbContext _context;

    public MoviesController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult>  Index()
    {
        List<Movie> allMovies = await _context.Movies.Include(n => n.Cinema).OrderBy(n => n.Name).ToListAsync();
        return View(allMovies);
    }
}