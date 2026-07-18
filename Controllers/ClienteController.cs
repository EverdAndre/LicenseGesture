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
            busca = busca.Trim();

            cliente = cliente.Where(p => EF.Functions.Like(p.Nome, $"%{busca}%"));
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

    // Get: Cliente/Create
    public IActionResult Create(string? returnUrl)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View(new Cliente { Ativo = true });
    }

    // Post : Cliente/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Cliente cliente, string? returnUrl)
    {
        try
        {
            if (ModelState.IsValid)
            {
                _context.Clientes.Add(cliente);
                _context.SaveChanges();
                if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return LocalRedirect(returnUrl);
                }
                return RedirectToAction("Index");
            }
            ViewData["ReturnUrl"] = returnUrl;
            return View(cliente);
        }
        catch
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View(cliente);
        }
    }

    // Get : Cliente/Edit
    public IActionResult Edit(int Id)
    {
        var cliente = _context.Clientes.Find(Id);
        if (cliente == null)
        {
            return NotFound("Cliente não encontrado.");
        }
        return View(cliente);
    }

    // Post : Cliente/Edit
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

    // Get : Cliente/Delete
    public IActionResult Delete(int Id)
    {
        var cliente = _context.Clientes.Find(Id);
        if (cliente == null)
        {
            return NotFound("Cliente não encontrado.");
        }
        return View(cliente);
    }

    // Post : Cliente/Delete
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmado(int id)
    {
        var clienteBanco = _context.Clientes.Find(id);
        if (clienteBanco == null)
        {
            return NotFound("Cliente não encontrado.");
        }
        clienteBanco.Ativo = false;
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    public IActionResult BuscarClientes(string? busca)
    {
        if (string.IsNullOrWhiteSpace(busca))
        {
            return Json(Array.Empty<object>());
        }

        busca = busca.Trim();

        var clientes = _context
            .Clientes.Where(p => p.Ativo && EF.Functions.Like(p.Nome, $"%{busca}%"))
            .OrderBy(p => p.Nome)
            .Take(10)
            .Select(p => new { id = p.Id, nome = p.Nome })
            .ToList();

        return Json(clientes);
    }
}
