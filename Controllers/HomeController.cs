using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using My_LinkShortener_App.Models;
using My_LinkShortener_App.Context;
using Microsoft.EntityFrameworkCore;

namespace My_LinkShortener_App.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly MyDbContext _context;

    public HomeController(ILogger<HomeController> logger, MyDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult MyLinksList()
    {
        return View(_context.Links.ToList());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult GetShort(Links model)
    {
        if (!ModelState.IsValid)
        {
            return View("Index",model);
        }
        model.ShortLink = $"http://localhost:5007/R/{model.ShortKey}";
        _context.Links.Add(model);
        _context.SaveChanges();
        ViewData["MasageSuccess"] = "Short link created!";
        return View("MyLinksList", _context.Links.ToList());
    }
    [HttpPost]
    public IActionResult Delete(int id)
    {
        var link = _context.Links.Find(id);
        if (link == null)
        {
            ViewData["MasageError"] = "Short link NotFound!";
            return View("MyLinksList",_context.Links.ToList());
        }
        else
        {
            _context.Entry(link).State = EntityState.Deleted;
            _context.SaveChanges();
            ViewData["MasageSuccess"] = "Short link Deleted!";
            return View("MyLinksList",_context.Links.ToList());
        }
    }

    [HttpGet]
    [Route("/R/{ShortKey}")]
    public IActionResult R(string ShortKey)
    {
        var model = _context.Links.FirstOrDefault(l => l.ShortKey == ShortKey);
        if (model == null)
        {
            return NotFound();
        }
        return Redirect(model.Link);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
