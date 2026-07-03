using System.Data.Common;
using LicenseGesture.Context;
using LicenseGesture.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

public class ClienteController : Controller
{
    private readonly LicenseDbContext _context;

    public ClienteController(LicenseDbContext context)
    {
        _context = context;
    }

    //retorna lista de clientes do bd
    public IActionResult Index(string? busca, string? ordenarPor, string? direcao)
    {
        var cliente = _context.Clientes.Where(p => p.Ativo).AsQueryable();
        ordenarPor = string.IsNullOrWhiteSpace(ordenarPor) ? "id" : ordenarPor;
        direcao = direcao == "desc" ? "desc" : "asc";

        if (!string.IsNullOrWhiteSpace(busca))
        {
            cliente = cliente.Where(p => p.Nome.Contains(busca));
        }
        cliente = (ordenarPor, direcao) switch
        {
            ("id", "desc") => cliente.OrderByDescending(p => p.Id),
            ("nome", "desc") => cliente.OrderByDescending(p => p.Nome),
            _ => cliente.OrderBy(p => p.Id),
        };
        ViewData["BuscaAtual"] = busca;
        ViewData["OrdenacaoAtual"] = ordenarPor;
        ViewData["DirecaoAtual"] = direcao;

        return View(cliente.ToList());
    }

    // Get: Produto/Create
    public IActionResult Create()
    {
        return View(new Cliente { Ativo = true });
    }

    // Post : Produto/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Cliente cliente)
    {
        try
        {
            if (ModelState.IsValid)
            {
                _context.Clientes.Add(cliente);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cliente);
        }
        catch
        {
            return View(cliente);
        }
    }

    // Get : Produto/Edit
    public IActionResult Edit(int Id)
    {
        var cliente = _context.Clientes.Find(Id);
        if (cliente == null)
        {
            return NotFound();
        }
        return View(cliente);
    }

    // Post : Produto/Edit
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Cliente cliente)
    {
        try
        {
            if (ModelState.IsValid)
            {
                _context.Clientes.Update(cliente);
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

    // Get : Produto/Delete
    public IActionResult Delete(int Id)
    {
        var cliente = _context.Clientes.Find(Id);
        if (cliente == null)
        {
            return NotFound();
        }
        return View(cliente);
    }

    // Post : Produto/Delete
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var clienteBanco = _context.Clientes.Find(id);
        if (clienteBanco == null)
        {
            return NotFound();
        }
        clienteBanco.Ativo = false;
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
}
