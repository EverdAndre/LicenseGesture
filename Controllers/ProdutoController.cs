using System.Data.Common;
using LicenseGesture.Context;
using LicenseGesture.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Diagnostics;

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
        var produtos = _context.Produtos.Where(p => p.Ativo).ToList();

        return View(produtos);
    }

    // Get: Produto/Create
    public IActionResult Create()
    {
        return View();
    }

    // Post : Produto/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Produto produto)
    {
        try
        {
            if (ModelState.IsValid)
            {
                _context.Produtos.Add(produto);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(produto);
        }
        catch
        {
            return View(produto);
        }
    }

    // Get : Produto/Edit
    public IActionResult Edit(int Id)
    {
        var produto = _context.Produtos.Find(Id);
        if (produto == null)
        {
            return NotFound();
        }
        return View(produto);
    }

    // Post : Produto/Edit
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Produto produto)
    {
        try
        {
            if (ModelState.IsValid)
            {
                _context.Produtos.Update(produto);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        catch
        {
            return View();
        }
    }
}
