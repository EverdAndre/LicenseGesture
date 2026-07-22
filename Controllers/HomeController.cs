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

    // Retorna a lista de vendas do banco de dados.
    public IActionResult Index(
        string? busca,
        string? ordenarPor,
        string? direcao)
    {
        var hoje = DateOnly.FromDateTime(DateTime.Today);

        var query = _context.Vendas
            .Include(v => v.Cliente)
            .Include(v => v.Produto)
            .AsQueryable();

        ordenarPor = string.IsNullOrWhiteSpace(ordenarPor)
            ? "id"
            : ordenarPor;

        direcao = direcao == "desc"
            ? "desc"
            : "asc";

        if (!string.IsNullOrWhiteSpace(busca))
        {
            busca = busca.Trim();

            query = query.Where(v =>
                EF.Functions.Like(v.Cliente.Nome, $"%{busca}%") ||
                EF.Functions.Like(v.Produto.Nome, $"%{busca}%"));
        }

        var vendas = query.ToList();

        vendas = (ordenarPor, direcao) switch
        {
            ("id", "desc") => vendas
                .OrderBy(v => ObterPrioridadeStatus(v.Anulada, v.ExpiraEm, hoje))
                .ThenByDescending(v => v.Id)
                .ToList(),

            ("cliente", "desc") => vendas
                .OrderBy(v => ObterPrioridadeStatus(v.Anulada, v.ExpiraEm, hoje))
                .ThenByDescending(v => v.Cliente.Nome)
                .ToList(),

            ("produto", "desc") => vendas
                .OrderBy(v => ObterPrioridadeStatus(v.Anulada, v.ExpiraEm, hoje))
                .ThenByDescending(v => v.Produto.Nome)
                .ToList(),

            ("valor", "desc") => vendas
                .OrderBy(v => ObterPrioridadeStatus(v.Anulada, v.ExpiraEm, hoje))
                .ThenByDescending(v => v.ValorFinalVenda ?? 0)
                .ToList(),

            ("validade", "desc") => vendas
                .OrderBy(v => ObterPrioridadeStatus(v.Anulada, v.ExpiraEm, hoje))
                .ThenByDescending(v => v.ExpiraEm)
                .ToList(),

            ("status", "desc") => vendas
                .OrderByDescending(v => ObterPrioridadeStatus(v.Anulada, v.ExpiraEm, hoje))
                .ThenBy(v => v.ExpiraEm)
                .ToList(),

            ("status", _) => vendas
                .OrderBy(v => ObterPrioridadeStatus(v.Anulada, v.ExpiraEm, hoje))
                .ThenBy(v => v.ExpiraEm)
                .ToList(),

            ("cliente", _) => vendas
                .OrderBy(v => ObterPrioridadeStatus(v.Anulada, v.ExpiraEm, hoje))
                .ThenBy(v => v.Cliente.Nome)
                .ToList(),

            ("produto", _) => vendas
                .OrderBy(v => ObterPrioridadeStatus(v.Anulada, v.ExpiraEm, hoje))
                .ThenBy(v => v.Produto.Nome)
                .ToList(),

            ("valor", _) => vendas
                .OrderBy(v => ObterPrioridadeStatus(v.Anulada, v.ExpiraEm, hoje))
                .ThenBy(v => v.ValorFinalVenda ?? 0)
                .ToList(),

            ("validade", _) => vendas
                .OrderBy(v => ObterPrioridadeStatus(v.Anulada, v.ExpiraEm, hoje))
                .ThenBy(v => v.ExpiraEm)
                .ToList(),

            _ => vendas
                .OrderBy(v => ObterPrioridadeStatus(v.Anulada, v.ExpiraEm, hoje))
                .ThenBy(v => v.ExpiraEm)
                .ToList()
        };

        ViewData["BuscaAtual"] = busca;
        ViewData["OrdenacaoAtual"] = ordenarPor;
        ViewData["DirecaoAtual"] = direcao;

        return View(vendas);
    }

    private static int ObterPrioridadeStatus(
        bool anulada,
        DateOnly expiraEm,
        DateOnly hoje)
    {
        if (anulada)
        {
            return 2;
        }

        if (expiraEm < hoje)
        {
            return 1;
        }

        return 0;
    }
}
