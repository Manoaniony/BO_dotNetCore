using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ArchiveNetCore.Models;
using ArchiveNetCore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace ArchiveNetCore.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ArchiveNetCoreContext _context;

    public HomeController(ILogger<HomeController> logger, ArchiveNetCoreContext context)
    {
        _logger = logger;
        _context = context;
    }


    [Authorize]
    public async Task<IActionResult> Index()
    {
        /*UtilClass res = new UtilClass();
        res.entrepots = _context.Entrepots != null ? await _context.Entrepots.ToListAsync() : new List<Entrepots>();
        res.articles = _context.Articles != null ? await _context.Articles.ToListAsync() : new List<Articles>();
        return View(res);*/
        return _context.Entrepots != null ?
                          View(await _context.Entrepots.ToListAsync()) :
                          Problem("Entity set 'ArchivagesContext.Entrepots'  is null.");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

