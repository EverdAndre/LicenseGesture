using System.Data.Common;
using LicenseGesture.Context;
using Microsoft.AspNetCore.Mvc;

public class ClienteController : Controller
{
    private readonly LicenseDbContext _context;

    public ClienteController(LicenseDbContext context)
    {
        _context = context;
    }
    public IActionResult Index()
    {
        var clientes = _context.Clientes.ToList();
        return View(clientes);
    }
}
