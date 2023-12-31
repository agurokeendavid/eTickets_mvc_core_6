﻿using eTickets.Data.Base;
using eTickets.Models;
using eTickets.ViewModels;

namespace eTickets.Data.Services;

public interface IMoviesService : IEntityBaseRepository<Movie>
{
    Task<Movie> GetMovieByIdAsync(int id);
    Task<NewMovieDropdownsViewModel> GetNewMovieDropdownsValuesAsync();
    Task AddNewMovieAsync(NewMovieViewModel viewModel);
    Task UpdateMovieAsync(NewMovieViewModel viewModel);
}