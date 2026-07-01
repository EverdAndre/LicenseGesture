using LicenseGesture.Context;
using LicenseGesture.Models;
using Microsoft.AspNetCore.Mvc;

public class ProdutoController : Controller
{
    private readonly LicenseDbContext _context;

    public ProdutoController(LicenseDbContext context)
    {
        _context = context;
    }
    //retorna lista de produtos do bd
    public IActionResult Index()
    {
        var produtos = _context.Produtos.ToList();
        return View(produtos);
    }
}
