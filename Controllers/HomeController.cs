using LicenseGesture.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class HomeController : Controller
{
    private readonly LicenseDbContext _context;

    public HomeController(LicenseDbContext context)
    {
        _context = context;
    }

    //retorna lista de vendas do bd
    public IActionResult Index(string? busca, string? ordenarPor, string? direcao)
    {
        var query = _context.Vendas.Include(p => p.Cliente).Include(p => p.Produto).AsQueryable();
        ordenarPor = string.IsNullOrWhiteSpace(ordenarPor) ? "id" : ordenarPor;
        direcao = direcao == "desc" ? "desc" : "asc";

        if (!string.IsNullOrWhiteSpace(busca))
        {
            busca = busca.Trim();
            query = query.Where(p =>
                EF.Functions.Like(p.Cliente.Nome, $"%{busca}%")
                || EF.Functions.Like(p.Produto.Nome, $"%{busca}%")
            );
        }
        var vendas = query.ToList();
        vendas = (ordenarPor, direcao) switch
        {
            ("id", "desc") => vendas.OrderByDescending(p => p.Id).ToList(),
            ("cliente", "desc") => vendas.OrderByDescending(p => p.Cliente.Nome).ToList(),
            ("produto", "desc") => vendas.OrderByDescending(p => p.Produto.Nome).ToList(),
            ("valor", "desc") => vendas.OrderByDescending(p => p.ValorFinalVenda ?? 0).ToList(),
            ("validade", "desc") => vendas.OrderByDescending(p => p.ExpiraEm).ToList(),
            ("cliente", _) => vendas.OrderBy(p => p.Cliente.Nome).ToList(),
            ("produto", _) => vendas.OrderBy(p => p.Produto.Nome).ToList(),
            ("valor", _) => vendas.OrderBy(p => p.ValorFinalVenda ?? 0).ToList(),
            ("validade", _) => vendas.OrderBy(p => p.ExpiraEm).ToList(),
            _ => vendas.OrderBy(p => p.Id).ToList(),
        };
        ViewData["BuscaAtual"] = busca;
        ViewData["OrdenacaoAtual"] = ordenarPor;
        ViewData["DirecaoAtual"] = direcao;

        return View(vendas);
    }
}
