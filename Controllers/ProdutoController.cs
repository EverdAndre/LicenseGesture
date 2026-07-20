using System.Data.Common;
using System.Threading.Tasks;
using LicenseGesture.Context;
using LicenseGesture.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

public class ProdutoController : Controller
{
    private readonly LicenseDbContext _context;

    public ProdutoController(LicenseDbContext context)
    {
        _context = context;
    }

    //retorna lista de produtos do bd
    public IActionResult Index(string? busca, string? ordenarPor, string? direcao)
    {
        var query = _context.Produtos.Where(p => p.Ativo).AsQueryable();
        ordenarPor = string.IsNullOrWhiteSpace(ordenarPor) ? "id" : ordenarPor;
        direcao = direcao == "desc" ? "desc" : "asc";

        if (!string.IsNullOrWhiteSpace(busca))
        {
            busca = busca.Trim();
            query = query.Where(p => EF.Functions.Like(p.Nome, $"%{busca}%"));
        }
        var produtos = query.ToList();
        produtos = (ordenarPor, direcao) switch
        {
            ("id", "desc") => produtos.OrderByDescending(p => p.Id).ToList(),
            ("nome", "desc") => produtos.OrderByDescending(p => p.Nome).ToList(),
            ("valor", "desc") => produtos.OrderByDescending(p => p.ValorVenda ?? 0).ToList(),
            ("validade", "desc") => produtos.OrderByDescending(p => p.Validade).ToList(),
            ("nome", _) => produtos.OrderBy(p => p.Nome).ToList(),
            ("valor", _) => produtos.OrderBy(p => p.ValorVenda ?? 0).ToList(),
            ("validade", _) => produtos.OrderBy(p => p.Validade).ToList(),
            _ => produtos.OrderBy(p => p.Id).ToList(),
        };
        ViewData["BuscaAtual"] = busca;
        ViewData["OrdenacaoAtual"] = ordenarPor;
        ViewData["DirecaoAtual"] = direcao;

        return View(produtos);
    }

    // Get: Produto/Create
    public IActionResult Create(string? returnUrl)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    // Post : Produto/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Produto produto, string? returnUrl)
    {
        try
        {
            if (ModelState.IsValid)
            {
                _context.Produtos.Add(produto);
                _context.SaveChanges();

                if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return LocalRedirect(returnUrl);
                }
                return RedirectToAction("Index");
            }
            ViewData["ReturnUrl"] = returnUrl;
            return View(produto);
        }
        catch
        {
            ViewData["ReturnUrl"] = returnUrl;
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

    // Get : Produto/Delete
    public IActionResult Delete(int Id)
    {
        var produto = _context.Produtos.Find(Id);
        if (produto == null)
        {
            return NotFound();
        }
        return View(produto);
    }

    // Post : Produto/Delete
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var produtoBanco = _context.Produtos.Find(id);
        if (produtoBanco == null)
        {
            return NotFound();
        }
        produtoBanco.Ativo = false;
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    public IActionResult BuscarProdutos(string? busca)
    {
        if (string.IsNullOrWhiteSpace(busca))
        {
            return Json(Array.Empty<object>());
        }

        busca = busca.Trim();

        var produtos = _context
            .Produtos.Where(p => p.Ativo && EF.Functions.Like(p.Nome, $"%{busca}%"))
            .OrderBy(p => p.Nome)
            .Take(10)
            .Select(p => new { id = p.Id, nome = p.Nome })
            .ToList();

        return Json(produtos);
    }
    public async Task <IActionResult> Details(int? id, string? returnUrl)
    {
        if (id == null)
        {
            return NotFound();
        }
        var produto = await _context.Produtos.FirstOrDefaultAsync(p => p.Id == id);
        if (produto == null)
        {
            return NotFound();
        }
        ViewBag.ReturnUrl = returnUrl;
        return View(produto);
    }

}
