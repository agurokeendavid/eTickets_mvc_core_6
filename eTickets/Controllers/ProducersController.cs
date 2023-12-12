using eTickets.Data.Services;
using eTickets.Data.Static;
using eTickets.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eTickets.Controllers;

[Authorize(Roles = UserRoles.Admin)]
public class ProducersController : Controller
{
    private readonly IProducersService _service;

    public ProducersController(IProducersService service)
    {
        _service = service;
    }

    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        IEnumerable<Producer> allProducers = await _service.GetAllAsync();
        return View(allProducers);
    }

    // GET: producers/details/1
    [AllowAnonymous]
    public async Task<IActionResult> Details(int id)
    {
        var producer = await _service.GetByIdAsync(id);

        if (producer is null)
            return View("NotFound");

        return View(producer);
    } 
    
    // Get: producers/create
    public async Task<IActionResult> Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("ProfilePictureURL", "FullName", "Bio")] Producer producer)
    {
        if (!ModelState.IsValid)
        {
            return View(producer);
        }

        await _service.AddAsync(producer);
        
        return RedirectToAction(nameof(Index));
    }
    
    public async Task<IActionResult> Edit(int id)
    {
        var producer = await _service.GetByIdAsync(id);

        if (producer is null)
            return View("NotFound");
        
        return View(producer);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id", "ProfilePictureURL", "FullName", "Bio")] Producer producer)
    {
        if (!ModelState.IsValid)
        {
            return View(producer);
        }

        if (id == producer.Id)
        {
            await _service.UpdateAsync(id, producer);
            return RedirectToAction(nameof(Index));
        }

        return View(producer);
    }
    
    public async Task<IActionResult> Delete(int id)
    {
        var producer = await _service.GetByIdAsync(id);
        
        if (producer is null)
        {
            return View("NotFound");
        }

        return View(producer);
    }
    
    [ValidateAntiForgeryToken]
    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var producer = await _service.GetByIdAsync(id);
        await _service.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}