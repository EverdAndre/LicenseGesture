using LicenseGesture.Context;
using LicenseGesture.Models;
using Microsoft.AspNetCore.Mvc;

public class VendaController : Controller
{
    private readonly LicenseDbContext _context;

    public VendaController(LicenseDbContext context)
    {
        _context = context;
    }
    public IActionResult Index()
    {
        var vendas = _context.Vendas.ToList();
        return View(vendas);
    }
}
