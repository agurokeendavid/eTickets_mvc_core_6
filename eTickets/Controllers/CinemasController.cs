using eTickets.Data.Services;
using eTickets.Data.Static;
using eTickets.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eTickets.Controllers;

[Authorize(Roles = UserRoles.Admin)]
public class CinemasController : Controller
{
    private readonly ICinemasService _service;

    public CinemasController(ICinemasService service)
    {
        _service = service;
    }

    [AllowAnonymous]
    public async Task<IActionResult>  Index()
    {
        IEnumerable<Cinema> allCinemas = await _service.GetAllAsync();
        return View(allCinemas);
    }

    // Get: Cinemas/Create
    public async Task<IActionResult> Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Logo", "Name", "Description")] Cinema cinema)
    {
        if (!ModelState.IsValid)
        {
            return View(cinema);
        }

        await _service.AddAsync(cinema);
        return RedirectToAction(nameof(Index));
    }

    // Get: cinema/details/1
    [AllowAnonymous]
    public async Task<IActionResult> Details(int id)
    {
        var cinema = await _service.GetByIdAsync(id);

        return View(cinema);

    }

    // Get: cinema/edit/1
    public async Task<IActionResult> Edit(int id)
    {
        var cinema = await _service.GetByIdAsync(id);

        if (cinema is null)
            return View("NotFound");

        return View(cinema);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id", "Logo", "Name", "Description")]Cinema cinema)
    {
        if (!ModelState.IsValid)
        {
            return View(cinema);
        }
        
        if (id == cinema.Id)
        {
            await _service.UpdateAsync(id, cinema);
            return RedirectToAction(nameof(Index));
        }
        
        return View(cinema);
    }
    
    // Get: Cinemas/Delete/1
    public async Task<IActionResult> Delete(int id)
    {
        var cinema = await _service.GetByIdAsync(id);
        
        if (cinema is null)
        {
            return View("NotFound");
        }

        return View(cinema);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var cinema = await _service.GetByIdAsync(id);
        if (cinema is null)
        {
            return View("NotFound");
        }
        await _service.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}